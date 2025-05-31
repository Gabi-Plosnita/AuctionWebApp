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

	private string? selectedDriverEmail;

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
		selectedDriverEmail = auction?.Driver?.Email ?? "No Driver";

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

	private async Task AssignDriver()
	{
		if (selectedDriverEmail == "No Driver")
		{
			Snackbar.ShowError("No driver selected");
			return;
		}

		var selectedDriver = drivers.FirstOrDefault(d => d.Email == selectedDriverEmail);
		if (selectedDriver == null)
		{
			Snackbar.ShowError("Selected driver not found");
			return;
		}

		var result = await AuctionService.AssignDriverAsync(AuctionId, selectedDriver.Id);
		if (result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
			return;
		}
		Snackbar.ShowSuccess("Driver assigned successfully");

		StateHasChanged();
	}
}
