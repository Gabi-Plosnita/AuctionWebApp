using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public interface IAuthHttpClient
{
	Task<Result<AdminResponse>> RegisterAdminAsync(RegisterAdminRequest request);
	
	Task<Result<string>> LoginAdminAsync(LoginRequest request);

	Task<Result<DriverResponse>> RegisterDriverAsync(RegisterDriverRequest request);
	
	Task<Result<string>> LoginDriverAsync(LoginRequest request);

	Task<Result<UserResponse>> RegisterUserAsync(RegisterUserRequest request);
	
	Task<Result<string>> LoginUserAsync(LoginRequest request);

	Task<Result> LogoutAsync();
}