using AuctionWebApp.Enums;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AuctionWebApp.Pages;

public partial class AdminDashboard : ComponentBase
{
	[Inject]
	private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

	private AuctionFilterViewModel auctionsWithNoDriverFilter = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InTransit,
		DriverFilterMode = DriverFilterMode.NoDriver,
	};

	private AuctionFilterViewModel auctionsWithDriverFilter = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InTransit,
		DriverFilterMode = DriverFilterMode.AnyDriver,
	};

	private string? navigateUrl = "auctions/{id}/assign-driver";

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
