using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Components;

public partial class AdminsTableComponent(IAdminService AdminService, ISnackbar Snackbar) : ComponentBase
{
	[Parameter]
	public int[] RowsPerPageOptions { get; set; } = new[] { 5, 10, 20 };

	private List<AdminViewModel> admins = new();

	private bool isLoading;

	protected override async Task OnInitializedAsync()
	{
		isLoading = true;
		var result = await AdminService.GetAllAsync();
		if (!result.HasErrors)
			admins = result.Data;
		else
		{
			Snackbar.ShowErrors(result.Errors);
		}
		isLoading = false;
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
