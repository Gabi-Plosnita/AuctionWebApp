using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AuctionWebApp.Pages;

public partial class DriversDashboard(AuthenticationStateProvider AuthenticationStateProvider,
									  NavigationManager NavigationManager) : ComponentBase
{
	private bool isSuperAdmin;

	private bool isLoading = true;

	protected override async Task OnInitializedAsync()
	{
		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		var user = authState.User;

		isSuperAdmin = user.IsInRole("SuperAdmin");
		isLoading = false;
	}

	private void NavigateToRegisterDriverPage()
	{
		NavigationManager.NavigateTo("admin/drivers-dashboard/create");
	}
}
