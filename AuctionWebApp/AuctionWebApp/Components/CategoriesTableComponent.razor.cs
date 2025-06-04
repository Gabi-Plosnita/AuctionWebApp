using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Components;

public partial class CategoriesTableComponent(ICategoryService CategoryService, 
											  NavigationManager NavigationManager, 
											  ISnackbar Snackbar) : ComponentBase
{
	[Parameter]
	public int[] RowsPerPageOptions { get; set; } = new[] { 5, 10, 20 };

	private List<CategoryViewModel> categories = new();

	private bool isLoading;

	protected override async Task OnInitializedAsync()
	{
		isLoading = true;
		var result = await CategoryService.GetAllAsync();
		if (!result.HasErrors)
			categories = result.Data;
		else
		{
			Snackbar.ShowErrors(result.Errors);
		}
		isLoading = false;
	}

	private void NavigateToEditCategoryPage(CategoryViewModel category)
	{
		NavigationManager.NavigateTo($"admin/categories-dashboard/{category.Id}/edit");
	}
}
