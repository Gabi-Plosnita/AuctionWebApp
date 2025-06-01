using AuctionWebApp.Enums;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class DriverDashboard : ComponentBase
{
	[Inject]
	private IAuthService AuthService { get; set; } = default!;

	private AuctionFilterViewModel auctionsFilter = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InTransit,
		DriverFilterMode = DriverFilterMode.Specific,
	};

	private string? navigateUrl = "auctions/{id}/complete";

	private string? navigationButtonName = "View";

	protected override async Task OnInitializedAsync()
	{
		var result = await AuthService.GetAuthenticatedUserAsync();
		if (result.HasErrors)
		{
			return;
		}
		if (result.Data == null)
		{
			return;
		}
		auctionsFilter.DriverId = result.Data.Id;
	}
}
