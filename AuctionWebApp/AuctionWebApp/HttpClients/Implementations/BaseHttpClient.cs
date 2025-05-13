using AuctionWebApp.Helpers;
using System.Net.Http.Json;
using System.Net;
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
				var data = await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
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
