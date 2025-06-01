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

	private string bidAuctionUrl = "/auctions/{id}/bid";

	private AuctionFilterViewModel myBidsAuctionFilter = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InProgress,
	};

	private AuctionsTableComponent bidAuctionsTable;

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
	}

	private async Task HandleBidStatusFilterChanged(AuctionStatus? status)
	{
		myBidsAuctionFilter.Status = status;
		await bidAuctionsTable.Reload();
	}


}
