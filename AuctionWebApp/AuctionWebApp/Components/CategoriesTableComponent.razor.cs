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

	private bool _loading;

	protected override async Task OnInitializedAsync()
	{
		_loading = true;
		var result = await CategoryService.GetAllAsync();
		if (!result.HasErrors)
			categories = result.Data;
		else
		{
			Snackbar.ShowErrors(result.Errors);
		}
		_loading = false;
	}

	private void NavigateToEditCategoryPage(CategoryViewModel category)
	{
		NavigationManager.NavigateTo($"/categories/edit/{category.Id}");
	}
}
