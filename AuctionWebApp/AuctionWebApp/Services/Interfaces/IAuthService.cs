using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface IAuthService
{
	Task<Result<AdminResponseViewModel>> RegisterAdminAsync(RegisterAdminRequestViewModel request);
	
	Task<Result> LoginAdminAsync(LoginRequestViewModel request);

	Task<Result<DriverResponseViewModel>> RegisterDriverAsync(RegisterDriverRequestViewModel request);
	
	Task<Result> LoginDriverAsync(LoginRequestViewModel request);

	Task<Result<UserResponseViewModel>> RegisterUserAsync(RegisterUserRequestViewModel request);
	
	Task<Result> LoginUserAsync(LoginRequestViewModel request);

	Task<Result> LogoutAsync();
}