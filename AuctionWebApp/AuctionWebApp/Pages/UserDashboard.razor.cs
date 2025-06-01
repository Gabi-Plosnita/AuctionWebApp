using AuctionWebApp.Components;
using AuctionWebApp.Enums;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class UserDashboard : ComponentBase
{
	[Inject]
	private IAuthService AuthService { get; set; } = default!;

	private int authenticatedUserId;

	// User Listed Auctions //

	private string editAuctionUrl = "/auctions/{id}/edit";

	private AuctionFilterViewModel myListingsAuctionFilter = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InProgress,
	};

	private AuctionsTableComponent listingsAuctionsTable;

	private async Task HandleListingStatusFilterChanged(AuctionStatus? status)
	{
		myListingsAuctionFilter.Status = status;
		await listingsAuctionsTable.Reload();
	}

	// User Bids //

	private string bidAuctionUrl = "/auctions/{id}/bid";

	private AuctionFilterViewModel myBidsAuctionFilter = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InProgress,
	};

	private AuctionsTableComponent bidsAuctionsTable;

	private async Task HandleBidStatusFilterChanged(AuctionStatus? status)
	{
		myBidsAuctionFilter.Status = status;
		await bidsAuctionsTable.Reload();
	}

	// Other methods //

	protected override async Task OnInitializedAsync()
	{
		var result = await AuthService.GetAuthenticatedUserAsync();
		if(result.HasErrors)
		{
			return;
		}
		if (result.Data == null)
		{
			return;
		}
		authenticatedUserId = result.Data.Id;
		myBidsAuctionFilter.BidderId = authenticatedUserId;
		myListingsAuctionFilter.SellerId = authenticatedUserId;
	}
}
