using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public class AuthHttpClient : BaseHttpClient, IAuthHttpClient
{
	public AuthHttpClient(HttpClient http) : base(http) { }

	public async Task<Result<AdminResponse>> RegisterAdminAsync(RegisterAdminRequest registerAdminRequest)
		=> await SendRequestAsync<AdminResponse>("api/auth/register/admins", HttpMethod.Post, registerAdminRequest);

	public async Task<Result> LoginAdminAsync(LoginRequest loginRequest)
		=> await SendRequestAsync("api/auth/login/admins", HttpMethod.Post, loginRequest);

	public async Task<Result<DriverResponse>> RegisterDriverAsync(RegisterDriverRequest registerDriverRequest)
		=> await SendRequestAsync<DriverResponse>("api/auth/register/drivers", HttpMethod.Post, registerDriverRequest);

	public async Task<Result> LoginDriverAsync(LoginRequest loginRequest)
		=> await SendRequestAsync("api/auth/login/drivers", HttpMethod.Post, loginRequest);

	public async Task<Result<UserResponse>> RegisterUserAsync(RegisterUserRequest registerUserRequest)
		=> await SendRequestAsync<UserResponse>("api/auth/register/users", HttpMethod.Post, registerUserRequest);

	public async Task<Result> LoginUserAsync(LoginRequest loginRequest)
		=> await SendRequestAsync("api/auth/login/users", HttpMethod.Post, loginRequest);

	public async Task<Result> LogoutAsync()
		=> await SendRequestAsync("api/auth/logout", HttpMethod.Post, null);

	public async Task<Result<AuthenticatedUserResponse>> GetAuthenticatedUserResponseAsync()
		=> await SendRequestAsync<AuthenticatedUserResponse>("api/auth/me", HttpMethod.Get, null);
}
