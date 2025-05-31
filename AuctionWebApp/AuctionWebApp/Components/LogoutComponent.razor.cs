using AuctionWebApp.Services;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class LogoutComponent : ComponentBase
{
	[Inject]
	private NavigationManager NavigationManager { get; set; } = default!;

	[Inject]

	private IAuthService AuthService { get; set; } = default!;

	private async Task LogoutAsync()
	{
		NavigationManager.NavigateTo("/");
		await AuthService.LogoutAsync();
	}
}
