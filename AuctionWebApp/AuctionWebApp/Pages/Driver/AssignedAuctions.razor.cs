using AuctionWebApp.Enums;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class AssignedAuctions(IAuthService AuthService) : ComponentBase
{
	private AuctionFilterViewModel auctionsFilter = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InTransit,
		DriverFilterMode = DriverFilterMode.Specific,
	};

	private string? navigateUrl = "driver/assigned-auctions/{id}/complete";

	private string? navigationButtonName = "View";

	private bool isLoading = true;

	protected override async Task OnInitializedAsync()
	{
		var result = await AuthService.GetAuthenticatedUserAsync();
		if (result.HasErrors)
		{
			isLoading = false;
			return;
		}
		if (result.Data == null)
		{
			isLoading = false;
			return;
		}
		auctionsFilter.DriverId = result.Data.Id;

		isLoading = false;
	}
}
