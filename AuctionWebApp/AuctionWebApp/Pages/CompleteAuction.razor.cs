using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class CompleteAuction : ComponentBase
{
	[Inject]
	private IDialogService DialogService { get; set; } = default!;

	[Inject]
	private ISnackbar Snackbar { get; set; } = default!;

	[Inject]
	private NavigationManager NavigationManager { get; set; } = default!;

	[Inject]
	private IAuctionService AuctionService { get; set; } = default!;

	[Parameter]
	public int AuctionId { get; set; }

	private string ReturnUrl { get; set; } = "/driver-dashboard";

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

	private async Task CompleteAuctionAsync()
	{
		bool? dialogResult = await DialogService.ShowMessageBox(
		"Confirm Completion",
		"Are you sure you want to complete this auction?",
		yesText: "Yes",
		cancelText: "Cancel");

		if (dialogResult == true)
		{
			var result = await AuctionService.CompleteAuctionAsync(AuctionId);
			if (result.HasErrors)
			{
				Snackbar.ShowErrors(result.Errors);
				return;
			}
			Snackbar.ShowSuccess("Auction completed successfully.");
			NavigationManager.NavigateTo(ReturnUrl, true);
		}
	}
}
