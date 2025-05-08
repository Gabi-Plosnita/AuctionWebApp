using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public interface IAuthHttpClient
{
	Task<Result<AdminResponse>> RegisterAdminAsync(RegisterAdminRequest request);
	
	Task<Result> LoginAdminAsync(LoginRequest request);

	Task<Result<DriverResponse>> RegisterDriverAsync(RegisterDriverRequest request);
	
	Task<Result> LoginDriverAsync(LoginRequest request);

	Task<Result<UserResponse>> RegisterUserAsync(RegisterUserRequest request);
	
	Task<Result> LoginUserAsync(LoginRequest request);

	Task<Result> LogoutAsync();
}