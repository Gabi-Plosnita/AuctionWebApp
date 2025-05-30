using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class AssignDriverToAuction : ComponentBase
{
	[Inject]
	private IDriverService DriverService { get; set; } = default!;

	[Inject]
	private IAuctionService AuctionService { get; set; } = default!;

	[Inject]
	private ISnackbar Snackbar { get; set; } = default!;

	[Parameter]
	public int AuctionId { get; set; }

	private DetailedAuctionViewModel auction;

	private List<DriverViewModel> drivers = new List<DriverViewModel>();

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

		var driversResult = await DriverService.GetAllAsync();
		if (driversResult.HasErrors)
		{
			Snackbar.ShowErrors(driversResult.Errors);
			return;
		}
		if (driversResult.Data is null)
		{
			Snackbar.ShowError("No drivers found");
			return;
		}
		drivers = driversResult.Data;
	}
}
