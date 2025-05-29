using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Components;

public partial class DriversTableComponent : ComponentBase
{
	[Inject]
	private IDriverService DriverService { get; set; } = default!;

	[Inject]
	private ISnackbar Snackbar { get; set; } = default!;

	[Inject]
	protected NavigationManager NavigationManager { get; set; } = default!;


	private List<DriverViewModel> drivers = new();

	private bool _loading;

	protected override async Task OnInitializedAsync()
	{
		_loading = true;
		var result = await DriverService.GetAllAsync();
		if (!result.HasErrors)
			drivers = result.Data;
		else
		{
			Snackbar.ShowErrors(result.Errors);
		}
		_loading = false;
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
