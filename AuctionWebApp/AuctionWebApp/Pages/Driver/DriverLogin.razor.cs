using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class DriverLogin(IAuthService AuthService, NavigationManager NavigationManager) : ComponentBase
{
	private string ReturnUrl { get; set; } = "/";

	private string RegisterUrl { get; set; } = "/driver/register";

	protected LoginViewModel LoginModel { get; set; } = new LoginViewModel();

	protected async Task<Result> HandleDriverLogin(LoginViewModel model)
	{
		var result = await AuthService.LoginDriverAsync(model);
		if (!result.HasErrors)
		{
			NavigationManager.NavigateTo("/driver/home");
		}
		return result;
	}
}
