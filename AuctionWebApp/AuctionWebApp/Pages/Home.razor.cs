using AuctionWebApp.Services;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class Home(IAuthService AuthService, NavigationManager NavigationManager) : ComponentBase
{
	private bool isLoading = true;
	protected override async Task OnInitializedAsync()
	{
		var authenticatedUserResult = await AuthService.GetAuthenticatedUserAsync();
		if (authenticatedUserResult.HasErrors)
		{
			isLoading = false;
			return;
		}

		if (authenticatedUserResult.Data == null)
		{
			isLoading = false;
			return;
		}

		var userRole = authenticatedUserResult.Data.Role;
		switch (userRole)
		{
			case "Admin":
				NavigationManager.NavigateTo("/admin/home");
				break;
			case "SuperAdmin":
				NavigationManager.NavigateTo("/admin/home");
				break;
			case "Driver":
				NavigationManager.NavigateTo("/driver/home");
				break;
			case "User":
				NavigationManager.NavigateTo("/user/home");
				break;
			default:
				break;
		}

		isLoading = false;
	}
}