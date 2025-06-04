using AuctionWebApp.Services;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class LogoutComponent(IAuthService AuthService, NavigationManager NavigationManager) : ComponentBase
{
	private async Task LogoutAsync()
	{
		NavigationManager.NavigateTo("/");
		await AuthService.LogoutAsync();
	}
}
