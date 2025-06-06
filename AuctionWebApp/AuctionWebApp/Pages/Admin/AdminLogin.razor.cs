using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class AdminLogin(IAuthService AuthService, NavigationManager Navigation) : ComponentBase
{
	private string ReturnUrl { get; set; } = "/";

	private string RegisterUrl { get; set; } = "/admin/register";

	protected LoginViewModel LoginModel { get; set; } = new LoginViewModel();

	protected async Task<Result> HandleAdminLogin(LoginViewModel model)
	{
		var result = await AuthService.LoginAdminAsync(model);
		if (!result.HasErrors)
		{
			Navigation.NavigateTo("admin/home");
		}
		return result;
	}
}
