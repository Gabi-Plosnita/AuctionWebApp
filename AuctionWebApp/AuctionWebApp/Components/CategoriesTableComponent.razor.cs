using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class CategoriesTableComponent
{
	[Inject] private ICategoryService CategoryService { get; set; } = default!;

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
			// show an error message, toast, etc.
		}
		_loading = false;
	}
}
