using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Components;

public partial class AdminsTableComponent : ComponentBase
{
	[Inject]
	private IAdminService AdminService { get; set; } = default!;

	[Inject]
	private ISnackbar Snackbar { get; set; } = default!;

	[Inject]
	protected NavigationManager NavigationManager { get; set; } = default!;


	private List<AdminViewModel> admins = new();

	private bool _loading;

	protected override async Task OnInitializedAsync()
	{
		_loading = true;
		var result = await AdminService.GetAllAsync();
		if (!result.HasErrors)
			admins = result.Data;
		else
		{
			Snackbar.ShowErrors(result.Errors);
		}
		_loading = false;
	}

	private async Task DeleteAdminAsync(int id)
	{
		var result = await AdminService.DeleteByIdAsync(id);
		if (result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
		}
		else
		{
			Snackbar.ShowSuccess("Admin deleted successfully.");
			admins.RemoveAll(admin => admin.Id == id);
		}
	}
}
