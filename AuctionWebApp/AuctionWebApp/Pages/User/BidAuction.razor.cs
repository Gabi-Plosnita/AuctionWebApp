using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class BidAuction(IAuctionService AuctionService,
								IAuthService AuthService,
								ISnackbar Snackbar) : ComponentBase
{
	[Parameter]
	public int AuctionId { get; set; }

	private DetailedAuctionViewModel auction;

	private CreateBidViewModel bid = new();

	private bool isLoading = true;

	private bool showErrorComponent = false;

	protected override async Task OnInitializedAsync()
	{
		await InitializeAuctionAsync();
		if (showErrorComponent || !isLoading)
			return;

		await AuthenticateUserAsync();
		if (showErrorComponent || !isLoading)
			return;

		bid.AuctionId = auction.Id;
		bid = new CreateBidViewModel
		{
			AuctionId = auction.Id,
		};

		isLoading = false;
	}

	private async Task SubmitBid()
	{
		var result = await AuctionService.CreateBidAsync(bid);

		if (result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
			return;
		}

		if (result.Data is null)
		{
			Snackbar.ShowError("Bid creation failed.");
			return;
		}

		Snackbar.ShowSuccess("Bid placed successfully!");

		await OnInitializedAsync();
	}

	private async Task AuthenticateUserAsync()
	{
		var authenticatedUserResult = await AuthService.GetAuthenticatedUserAsync();
		if (authenticatedUserResult.HasErrors
			|| authenticatedUserResult.Data is null
			|| authenticatedUserResult.Data.Id == auction.Seller.Id)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}
	}

	private async Task InitializeAuctionAsync()
	{
		var auctionResult = await AuctionService.GetDetailedByIdAsync(AuctionId);
		if (auctionResult.HasErrors || auctionResult.Data is null)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}
		auction = auctionResult.Data;
	}
}
