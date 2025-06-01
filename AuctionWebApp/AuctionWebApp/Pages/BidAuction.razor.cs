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

	private bool isLoading = true;

	protected override async Task OnInitializedAsync()
	{
		var auctionResult = await AuctionService.GetDetailedByIdAsync(AuctionId);
		if (auctionResult.HasErrors)
		{
			Snackbar.ShowErrors(auctionResult.Errors);
			return;
		}
		if (auctionResult.Data is null)
		{
			Snackbar.ShowError("Auction not found");
			return;
		}
		auction = auctionResult.Data;

		isLoading = false;
	}
}
