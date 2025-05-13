using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public class AuthHttpClient : BaseHttpClient, IAuthHttpClient
{
	public AuthHttpClient(HttpClient http) : base(http) { }

	public async Task<Result<AdminResponse>> RegisterAdminAsync(RegisterAdminRequest request)
		=> await SendRequestAsync<AdminResponse>("api/auth/register/admins", HttpMethod.Post, request);

	public async Task<Result> LoginAdminAsync(LoginRequest request)
		=> await SendRequestAsync("api/auth/login/admins", HttpMethod.Post, request);

	public async Task<Result<DriverResponse>> RegisterDriverAsync(RegisterDriverRequest request)
		=> await SendRequestAsync<DriverResponse>("api/auth/register/drivers", HttpMethod.Post, request);

	public async Task<Result> LoginDriverAsync(LoginRequest request)
		=> await SendRequestAsync("api/auth/login/drivers", HttpMethod.Post, request);

	public async Task<Result<UserResponse>> RegisterUserAsync(RegisterUserRequest request)
		=> await SendRequestAsync<UserResponse>("api/auth/register/users", HttpMethod.Post, request);

	public async Task<Result> LoginUserAsync(LoginRequest request)
		=> await SendRequestAsync("api/auth/login/users", HttpMethod.Post, request);

	public async Task<Result> LogoutAsync()
		=> await SendRequestAsync("api/auth/logout", HttpMethod.Post, null);
}
