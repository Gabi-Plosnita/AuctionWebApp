using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public class AuthHttpClient : BaseHttpClient, IAuthHttpClient
{
	public AuthHttpClient(HttpClient http) : base(http) { }

	public Task<Result<AdminResponse>> RegisterAdminAsync(RegisterAdminRequest request)
		=> SendWithResponseAsync<AdminResponse>("api/auth/register/admins", HttpMethod.Post, request);

	public Task<Result<string>> LoginAdminAsync(LoginRequest request)
		=> SendWithResponseAsync<string>("api/auth/login/admins", HttpMethod.Post, request);

	public Task<Result<DriverResponse>> RegisterDriverAsync(RegisterDriverRequest request)
		=> SendWithResponseAsync<DriverResponse>("api/auth/register/drivers", HttpMethod.Post, request);

	public Task<Result<string>> LoginDriverAsync(LoginRequest request)
		=> SendWithResponseAsync<string>("api/auth/login/drivers", HttpMethod.Post, request);

	public Task<Result<UserResponse>> RegisterUserAsync(RegisterUserRequest request)
		=> SendWithResponseAsync<UserResponse>("api/auth/register/users", HttpMethod.Post, request);

	public Task<Result<string>> LoginUserAsync(LoginRequest request)
		=> SendWithResponseAsync<string>("api/auth/login/users", HttpMethod.Post, request);

	public Task<Result> LogoutAsync()
		=> SendNoResponseAsync("api/auth/logout", HttpMethod.Post, null);
}
