using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class AdminLogin : ComponentBase
{
	[Inject]
	public IAuthService _authService { get; set; } = default!;
	protected LoginViewModel AdminModel { get; set; } = new LoginViewModel();

	protected async Task HandleAdminLogin(LoginViewModel model)
	{
		var result = await _authService.LoginAdminAsync(model);
	}
}
