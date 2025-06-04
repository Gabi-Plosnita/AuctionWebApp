using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class CompleteAuction(IAuctionService AuctionService,
									 IAuthService AuthService,
									 NavigationManager NavigationManager,
									 IDialogService DialogService,
									 ISnackbar Snackbar) : ComponentBase
{
	[Parameter]
	public int AuctionId { get; set; }

	private DetailedAuctionViewModel auction;

	private bool isLoading = true;

	private bool isAuthorized = true;

	private string ReturnUrl = "driver/assigned-auctions";

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

		await AuthorizeAuthenticatedDriverAsync();

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
			NavigationManager.NavigateTo(ReturnUrl);
		}
	}

	private async Task AuthorizeAuthenticatedDriverAsync()
	{
		var authenticatedUserResult = await AuthService.GetAuthenticatedUserAsync();
		if (authenticatedUserResult.HasErrors)
		{
			Snackbar.ShowErrors(authenticatedUserResult.Errors);
			isAuthorized = false;
			return;
		}
		var authenticatedUser = authenticatedUserResult.Data;
		if (authenticatedUser is null)
		{
			isAuthorized = false;
			return;
		}
		if (auction.Driver is null || auction.Driver.Id != authenticatedUser.Id)
		{
			isAuthorized = false;
			return;
		}
	}
}
