using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class AuctionDetails(IAuctionService AuctionService) : ComponentBase
{
	[Parameter]
	public int AuctionId { get; set; }

	private DetailedAuctionViewModel auction;

	private bool isLoading = true;

	private bool showErrorComponent = false;

	protected override async Task OnInitializedAsync()
	{
		await InitializeAuctionAsync();
		if (showErrorComponent || !isLoading)
			return;

		isLoading = false;
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
