using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class UserLogin(IAuthService AuthService, NavigationManager NavigationManager) : ComponentBase
{
	private string ReturnUrl { get; set; } = "/";

	protected LoginViewModel LoginModel { get; set; } = new LoginViewModel();

	protected async Task<Result> HandleUserLogin(LoginViewModel model)
	{
		var result = await AuthService.LoginUserAsync(model);
		if (!result.HasErrors)
		{
			NavigationManager.NavigateTo("/user/home");
		}
		return result;
	}
}
