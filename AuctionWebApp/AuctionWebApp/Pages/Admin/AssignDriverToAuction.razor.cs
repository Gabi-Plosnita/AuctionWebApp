using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class AssignDriverToAuction(IDriverService DriverService,
										   IAuctionService AuctionService,
										   AuthenticationStateProvider AuthenticationStateProvider,
										   ISnackbar Snackbar) : ComponentBase
{
	[Parameter]
	public int AuctionId { get; set; }

	private string ReturnUrl { get; set; } = "/admin/auctions-dashboard";

	private DetailedAuctionViewModel auction;

	private List<DriverViewModel> drivers = new List<DriverViewModel>();

	private string? selectedDriverEmail;

	private bool isSuperAdmin;

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

		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		var user = authState.User;

		isSuperAdmin = user.IsInRole("SuperAdmin");
		isLoading = false;
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

		auction.Driver = selectedDriver;
	}
}
