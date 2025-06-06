using AuctionWebApp.Components;
using AuctionWebApp.Enums;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class UserListings(IAuthService AuthService) : ComponentBase
{
	private string viewAuctionUrl = "user/auctions/{id}/view";

	private AuctionFilterViewModel myListingsAuctionFilter = new AuctionFilterViewModel
	{
		Status = null,
	};

	private AuctionsTableComponent listingsAuctionsTable;

	private bool isLoading = true;

	private bool showErrorComponent = false;

	private async Task HandleListingStatusFilterChanged(AuctionStatus? status)
	{
		myListingsAuctionFilter.Status = status;
		await listingsAuctionsTable.Reload();
	}

	protected override async Task OnInitializedAsync()
	{
		var result = await AuthService.GetAuthenticatedUserAsync();
		if (result.HasErrors)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}
		if (result.Data == null)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}
		myListingsAuctionFilter.SellerId = result.Data.Id;

		isLoading = false;
	}
}
