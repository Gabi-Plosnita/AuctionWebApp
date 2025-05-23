using AuctionWebApp.Helpers;
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

	[Inject]
	private JwtAuthStateProvider AuthStateProvider { get; set; } = default!;

	protected LoginViewModel AdminModel { get; set; } = new LoginViewModel();

	protected async Task<Result> HandleAdminLogin(LoginViewModel model)
	{
		var result = await _authService.LoginAdminAsync(model);
		if (!result.HasErrors)
		{
			AuthStateProvider.NotifyAuthChanged();
			Navigation.NavigateTo("/admin-dashboard");
		}
		return result;
	}
}
