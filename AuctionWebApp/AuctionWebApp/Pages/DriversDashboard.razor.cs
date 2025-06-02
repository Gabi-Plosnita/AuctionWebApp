using AuctionWebApp.Components;
using AuctionWebApp.Enums;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class DriversDashboard : ComponentBase
{
	[Inject]
	private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

	private bool isSuperAdmin;

	private bool isLoading = true;

	protected override async Task OnInitializedAsync()
	{
		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		var user = authState.User;

		isSuperAdmin = user.IsInRole("SuperAdmin");
		isLoading = false;
	}
}
