using AuctionWebApp.Helpers;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace AuctionWebApp.HttpClients;

public abstract class BaseHttpClient
{
	protected readonly HttpClient _http;

	private static readonly JsonSerializerOptions _jsonOptions = new()
	{
		PropertyNameCaseInsensitive = true
	};

	protected BaseHttpClient(HttpClient http) => _http = http;

	protected async Task<Result<T>> SendRequestAsync<T>(string url, HttpMethod method, object? body = null)
	{
		using var request = new HttpRequestMessage(method, url);
		if (body is not null)
			request.Content = JsonContent.Create(body, options: _jsonOptions);

		HttpResponseMessage response;
		try
		{
			response = await _http.SendAsync(request);
		}
		catch (Exception ex)
		{
			return new Result<T> { Errors = new List<string> { ex.Message } };
		}

		using (response)
		{
			if (!response.IsSuccessStatusCode)
				return new Result<T> { Errors = await ExtractErrorsAsync(response) };

			if (response.StatusCode == HttpStatusCode.NoContent || response.Content.Headers.ContentLength == 0)
				return new Result<T>();

			try
			{
				var rawContent = await response.Content.ReadAsStringAsync();

				if (typeof(T) == typeof(string))
				{
					return new Result<T> { Data = (T)(object)rawContent };
				}

				var data = JsonSerializer.Deserialize<T>(rawContent, _jsonOptions);
				if (data is null)
					return new Result<T> { Errors = new List<string> { "Empty or invalid response body." } };

				return new Result<T> { Data = data };
			}
			catch (Exception ex)
			{
				return new Result<T> { Errors = new List<string> { ex.Message } };
			}
		}
	}



	protected async Task<Result> SendRequestAsync(string url, HttpMethod method, object? body = null)
	{
		using var request = new HttpRequestMessage(method, url);
		if (body is not null)
			request.Content = JsonContent.Create(body, options: _jsonOptions);

		HttpResponseMessage response;
		try
		{
			response = await _http.SendAsync(request);
		}
		catch (Exception ex)
		{
			return new Result { Errors = new List<string> { ex.Message } };
		}

		using (response)
		{
			if (response.IsSuccessStatusCode)
				return new Result();

			return new Result { Errors = await ExtractErrorsAsync(response) };
		}
	}

	protected async Task<Result<T>> SendRequestWithQueryAsync<T>(string url, HttpMethod method, object? filter = null)
	{
		if (filter is not null)
		{
			var queryParams = filter
				.GetType()
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Select(prop => new
				{
					Name = prop.Name,
					Value = prop.GetValue(filter, null)
				})
				.Where(x => x.Value is not null)
				.ToDictionary(
					x => x.Name,
					x => x.Value!.ToString()!,
					StringComparer.OrdinalIgnoreCase);

			url = QueryHelpers.AddQueryString(url, queryParams);
		}

		return await SendRequestAsync<T>(url, method);
	}

	protected async Task<Result<T>> SendFormRequestAsync<T>(string url, HttpMethod method, object formObject)
	{
		if (formObject is null)
			return new Result<T> { Errors = new List<string> { "Form object cannot be null." } };

		using var request = new HttpRequestMessage(method, url);
		request.Content = BuildMultipart(formObject);

		HttpResponseMessage response;
		try
		{
			response = await _http.SendAsync(request);
		}
		catch (Exception ex)
		{
			return new Result<T> { Errors = new List<string> { ex.Message } };
		}

		using (response)
		{
			if (!response.IsSuccessStatusCode)
				return new Result<T> { Errors = await ExtractErrorsAsync(response) };

			if (response.StatusCode == HttpStatusCode.NoContent
			 || response.Content.Headers.ContentLength == 0)
				return new Result<T>();

			try
			{
				var data = await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
				return data is null
					? new Result<T> { Errors = new List<string> { "Empty or invalid response body." } }
					: new Result<T> { Data = data };
			}
			catch (Exception ex)
			{
				return new Result<T> { Errors = new List<string> { ex.Message } };
			}
		}
	}

	protected async Task<Result> SendFormRequestAsync(string url, HttpMethod method, object formObject)
	{
		if (formObject is null)
			return new Result { Errors = new List<string> { "Form object cannot be null." } };

		using var request = new HttpRequestMessage(method, url);
		request.Content = BuildMultipart(formObject);

		HttpResponseMessage response;
		try
		{
			response = await _http.SendAsync(request);
		}
		catch (Exception ex)
		{
			return new Result { Errors = new List<string> { ex.Message } };
		}

		using (response)
		{
			if (response.IsSuccessStatusCode)
				return new Result();

			return new Result { Errors = await ExtractErrorsAsync(response) };
		}
	}

	private MultipartFormDataContent BuildMultipart(object formObject)
	{
		var multipart = new MultipartFormDataContent();
		var props = formObject.GetType()
							  .GetProperties(BindingFlags.Public | BindingFlags.Instance);

		foreach (var prop in props)
		{
			var value = prop.GetValue(formObject);
			if (value is null) continue;

			if (value is IBrowserFile singleFile)
			{
				AddFilePart(multipart, prop.Name, singleFile);
				continue;
			}

			if (value is IEnumerable<IBrowserFile> files)
			{
				foreach (var file in files)
					AddFilePart(multipart, prop.Name, file);
				continue;
			}

			string strValue;
			if (value is DateTime dt)
				strValue = dt.ToString("o"); 
			else if (value is DateTimeOffset dto)
				strValue = dto.ToString("o");
			else
				strValue = value.ToString()!;

			multipart.Add(new StringContent(strValue, Encoding.UTF8), prop.Name);
		}

		return multipart;
	}


	private void AddFilePart(MultipartFormDataContent multipart, string fieldName, IBrowserFile file)
	{
		const long maxAllowedSize = 2 * 1024 * 1024; 
		var stream = file.OpenReadStream(maxAllowedSize);
		var content = new StreamContent(stream);

		content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);	

		multipart.Add(content, fieldName, file.Name);
	}

	private static async Task<List<string>> ExtractErrorsAsync(HttpResponseMessage response)
	{
		if (response.StatusCode == HttpStatusCode.Unauthorized)
			return new List<string> { "User is not authenticated." };

		if (response.StatusCode == HttpStatusCode.Forbidden)
			return new List<string> { "User is not authorized to perform this action." };
		int status = (int)response.StatusCode;

		if (status >= 500 && status < 600)
			return new List<string> { "A server error occurred. Please try again later." };
		var content = await response.Content.ReadAsStringAsync();

		try
		{
			using var doc = JsonDocument.Parse(content);
			if (doc.RootElement.TryGetProperty("errors", out var errorsElem)
			 && errorsElem.ValueKind == JsonValueKind.Object)
			{
				var list = new List<string>();
				foreach (var prop in errorsElem.EnumerateObject())
					foreach (var item in prop.Value.EnumerateArray())
						if (item.ValueKind == JsonValueKind.String)
							list.Add(item.GetString()!);
				if (list.Count > 0)
					return list;
			}
		}
		catch { }

		try
		{
			var apiErrors = JsonSerializer.Deserialize<List<string>>(content, _jsonOptions);
			if (apiErrors is { Count: > 0 })
				return apiErrors;
		}
		catch { }

		return new List<string>
		{
			content?.Trim('"') ?? response.ReasonPhrase ?? "Unknown error"
		};
	}
}