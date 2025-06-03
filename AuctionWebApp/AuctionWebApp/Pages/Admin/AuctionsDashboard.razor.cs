using AuctionWebApp.Components;
using AuctionWebApp.Enums;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AuctionWebApp.Pages;

public partial class AuctionsDashboard(AuthenticationStateProvider AuthenticationStateProvider) : ComponentBase
{
	private AuctionFilterViewModel auctionFilter = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InTransit,
		DriverFilterMode = DriverFilterMode.Ignore,
	};

	private AuctionsTableComponent auctionsTable;

	private async Task HandleDriverFilterChanged(DriverFilterMode status)
	{
		auctionFilter.DriverFilterMode = status;
		await auctionsTable.Reload();
	}

	private string? navigateUrl = "admin/auctions-dashboard/{id}/assign-driver";

	private string? navigationButtonName = "View";

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
