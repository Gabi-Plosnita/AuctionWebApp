using AuctionWebApp.Components;
using AuctionWebApp.Enums;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class UserBids(IAuthService AuthService) : ComponentBase
{
	private bool showErrorComponent = false;

	private string bidAuctionUrl = "user/auctions/{id}/bid";

	private AuctionFilterViewModel myBidsAuctionFilter = new AuctionFilterViewModel
	{
		Status = null,
	};

	private AuctionsTableComponent bidsAuctionsTable;

	private async Task HandleBidStatusFilterChanged(AuctionStatus? status)
	{
		myBidsAuctionFilter.Status = status;
		await bidsAuctionsTable.Reload();
	}

	protected override async Task OnInitializedAsync()
	{
		var result = await AuthService.GetAuthenticatedUserAsync();
		if (result.HasErrors)
		{
			showErrorComponent = true;
			return;
		}
		if (result.Data == null)
		{
			showErrorComponent = true;
			return;
		}
		myBidsAuctionFilter.BidderId = result.Data.Id;
	}
}
