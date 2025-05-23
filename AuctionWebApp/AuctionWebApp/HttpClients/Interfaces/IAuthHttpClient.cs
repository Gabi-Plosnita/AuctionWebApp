using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public interface IAuthHttpClient
{
	Task<Result<AdminResponse>> RegisterAdminAsync(RegisterAdminRequest registerAdminRequest);
	
	Task<Result> LoginAdminAsync(LoginRequest loginRequest);

	Task<Result<DriverResponse>> RegisterDriverAsync(RegisterDriverRequest registerDriverRequest);
	
	Task<Result> LoginDriverAsync(LoginRequest loginRequest);

	Task<Result<UserResponse>> RegisterUserAsync(RegisterUserRequest registerUserRequest);
	
	Task<Result> LoginUserAsync(LoginRequest loginRequest);

	Task<Result> LogoutAsync();

	Task<Result<AuthenticatedUserResponse>> GetAuthenticatedUserResponseAsync();
}