using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class CategoriesTableComponent
{
	[Inject]
	private ICategoryService CategoryService { get; set; } = default!;

	private List<CategoryViewModel> categories = new();

	protected override async Task OnInitializedAsync()
	{
		var result = await CategoryService.GetAllAsync();
		if (!result.HasErrors)
		{
			categories = result.Data;
		}
		else
		{
			// Handle error, e.g., show a message to the user
		}
	}
}
