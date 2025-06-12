using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface IAuthService
{
	Task<Result<AdminViewModel>> RegisterAdminAsync(RegisterAdminViewModel registerAdminViewModel);
	
	Task<Result> LoginAdminAsync(LoginViewModel loginViewModel);

	Task<Result<DriverViewModel>> RegisterDriverAsync(RegisterDriverViewModel registerDriverViewModel);
	
	Task<Result> LoginDriverAsync(LoginViewModel loginViewModel);

	Task<Result<UserViewModel>> RegisterUserAsync(RegisterUserViewModel registerUserViewModel);
	
	Task<Result> LoginUserAsync(LoginViewModel loginViewModel);

	Task<Result> RequestPasswordResetAsync(PasswordResetViewModel passwordResetViewModel);

	Task<Result> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel);

	Task<Result<AuthenticatedUserViewModel>> GetAuthenticatedUserAsync();

	Task<Result> LogoutAsync();
}