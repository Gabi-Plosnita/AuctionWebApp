using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Components;

public partial class DriversTableComponent(IDriverService DriverService, ISnackbar Snackbar) : ComponentBase
{
	[Parameter]
	public int[] RowsPerPageOptions { get; set; } = new[] { 5, 10, 20 };

	private List<DriverViewModel> drivers = new();

	private bool isLoading;

	protected override async Task OnInitializedAsync()
	{
		isLoading = true;
		var result = await DriverService.GetAllAsync();
		if (!result.HasErrors)
			drivers = result.Data;
		else
		{
			Snackbar.ShowErrors(result.Errors);
		}
		isLoading = false;
	}

	private async Task DeleteDriverAsync(int id)
	{
		var result = await DriverService.DeleteByIdAsync(id);
		if (result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
		}
		else
		{
			Snackbar.ShowSuccess("Driver deleted successfully.");
			drivers.RemoveAll(driver => driver.Id == id);
		}
	}
}
