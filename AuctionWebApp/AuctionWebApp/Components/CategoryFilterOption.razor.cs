using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class CategoryFilterOption : ComponentBase
{
	[Parameter]
	public CategoryViewModel Category { get; set; } = new();

	[Parameter]
	public EventCallback<CategoryViewModel> OnCategoryClicked { get; set; }

	private async Task HandleCardClickAsync()
	{
		if (OnCategoryClicked.HasDelegate)
		{
			await OnCategoryClicked.InvokeAsync(Category);
		}
	}
}
