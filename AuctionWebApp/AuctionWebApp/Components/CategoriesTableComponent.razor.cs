using AuctionWebApp.Enums;
using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Components;

public partial class CategoriesTableComponent
{
	[Inject] private ICategoryService CategoryService { get; set; } = default!;

	[Inject] private ISnackbar Snackbar { get; set; } = default!;

	[Inject] protected NavigationManager NavigationManager { get; set; } = default!;


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

	private void NavigateToCreateCategoryPage()
	{
		NavigationManager.NavigateTo("/categories-create");
	}

	private void EditCategory(CategoryViewModel category)
	{
		// For example: navigate to an edit page or open a dialog
		//NavigationManager.NavigateTo($"/edit-category/{category.Id}");

		// OR: Open a MudDialog to edit inline (if using MudDialogService)
		// await DialogService.Show<EditCategoryDialog>("Edit Category", new DialogParameters { ["Category"] = category });
	}

}
