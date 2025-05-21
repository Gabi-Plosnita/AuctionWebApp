using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class AdminLogin : ComponentBase
{
	[Inject]
	public IAuthService _authService { get; set; } = default!;

	[Inject]
	public NavigationManager Navigation { get; set; } = default!;

	protected LoginViewModel AdminModel { get; set; } = new LoginViewModel();
	protected bool ShowPopup { get; set; }
	protected List<string> ErrorMessages { get; set; } = new List<string>();

	protected async Task HandleAdminLogin(LoginViewModel model)
	{
		var result = await _authService.LoginAdminAsync(model);
		if (!result.HasErrors)
		{
			Navigation.NavigateTo("/");
		}
		else
		{
			ErrorMessages = result.Errors.ToList();
			ShowPopup = true;
		}
	}

	void ClosePopup()
	{
		ShowPopup = false;
	}
}
