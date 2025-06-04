using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class BidAuction(IAuctionService AuctionService, ISnackbar Snackbar) : ComponentBase
{
	[Parameter]
	public int AuctionId { get; set; }

	private DetailedAuctionViewModel auction;

	private CreateBidViewModel bid = new();

	private bool isLoading = true;

	private bool showErrorComponent = false;

	protected override async Task OnInitializedAsync()
	{
		var auctionResult = await AuctionService.GetDetailedByIdAsync(AuctionId);
		if (auctionResult.HasErrors)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}
		if (auctionResult.Data is null)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}
		auction = auctionResult.Data;
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

}
