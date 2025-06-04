using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class BidAuction : ComponentBase
{
	[Inject]
	private IAuctionService AuctionService { get; set; } = default!;

	[Inject]
	private ISnackbar Snackbar { get; set; } = default!;

	[Parameter]
	public int AuctionId { get; set; }

	private string ReturnUrl { get; set; } = "/user-dashboard";

	private DetailedAuctionViewModel auction;

	private CreateBidViewModel bid = new();

	private bool isLoading = true;

	protected override async Task OnInitializedAsync()
	{
		var auctionResult = await AuctionService.GetDetailedByIdAsync(AuctionId);
		if (auctionResult.HasErrors)
		{
			Snackbar.ShowErrors(auctionResult.Errors);
			isLoading = false;
			return;
		}
		if (auctionResult.Data is null)
		{
			Snackbar.ShowError("Auction not found");
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
