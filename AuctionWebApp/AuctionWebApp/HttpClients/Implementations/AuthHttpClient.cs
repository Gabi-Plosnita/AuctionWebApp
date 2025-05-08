using AuctionWebApp.Helpers;
using AuctionWebApp.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace AuctionWebApp.HttpClients;

public class AuthHttpClient : IAuthHttpClient
{
	private readonly HttpClient _http;
	private static readonly JsonSerializerOptions _jsonOptions =
		new() { PropertyNameCaseInsensitive = true };

	public AuthHttpClient(HttpClient http) => _http = http;

	public Task<Result<AdminResponse>> RegisterAdminAsync(RegisterAdminRequest request)
		=> SendAsync<AdminResponse>("api/auth/register/admins", HttpMethod.Post, request);

	public Task<Result<string>> LoginAdminAsync(LoginRequest request)
		=> SendAsync<string>("api/auth/login/admins", HttpMethod.Post, request);

	public Task<Result<DriverResponse>> RegisterDriverAsync(RegisterDriverRequest request)
		=> SendAsync<DriverResponse>("api/auth/register/drivers", HttpMethod.Post, request);

	public Task<Result<string>> LoginDriverAsync(LoginRequest request)
		=> SendAsync<string>("api/auth/login/drivers", HttpMethod.Post, request);

	public Task<Result<UserResponse>> RegisterUserAsync(RegisterUserRequest request)
		=> SendAsync<UserResponse>("api/auth/register/users", HttpMethod.Post, request);

	public Task<Result<string>> LoginUserAsync(LoginRequest request)
		=> SendAsync<string>("api/auth/login/users", HttpMethod.Post, request);

	public async Task<Result> LogoutAsync()
	{
		var response = await _http.PostAsync("api/auth/logout", null);
		if (response.IsSuccessStatusCode)
			return new Result();
		return new Result { Errors = await ExtractErrorsAsync(response) };
	}

	private async Task<Result<T>> SendAsync<T>(string url, HttpMethod method, object? body = null)
	{
		using var request = new HttpRequestMessage(method, url);
		if (body is not null)
			request.Content = JsonContent.Create(body, options: _jsonOptions);

		using var response = await _http.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			var data = await response.Content.ReadFromJsonAsync<T>(_jsonOptions)!;
			return new Result<T> { Data = data };
		}
		else
		{
			return new Result<T> { Errors = await ExtractErrorsAsync(response) };
		}
	}

	private static async Task<List<string>> ExtractErrorsAsync(HttpResponseMessage response)
	{
		var content = await response.Content.ReadAsStringAsync();
		try
		{
			var apiErrors = JsonSerializer.Deserialize<List<string>>(content, _jsonOptions);
			if (apiErrors?.Count > 0)
				return apiErrors;
		}
		catch { }

		return new List<string> { content?.Trim('"') ?? response.ReasonPhrase ?? "Unknown error" };
	}
}
