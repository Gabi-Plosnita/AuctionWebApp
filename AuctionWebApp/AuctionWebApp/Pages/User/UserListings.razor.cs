using AuctionWebApp.Components;
using AuctionWebApp.Enums;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class UserListings(IAuthService AuthService) : ComponentBase
{
	private bool showErrorComponent = false;

	private string editAuctionUrl = "user/my-listings/{id}/edit";

	private AuctionFilterViewModel myListingsAuctionFilter = new AuctionFilterViewModel
	{
		Status = null,
	};

	private AuctionsTableComponent listingsAuctionsTable;

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
			return;
		}
		if (result.Data == null)
		{
			showErrorComponent = true;
			return;
		}
		myListingsAuctionFilter.SellerId = result.Data.Id;
	}
}
