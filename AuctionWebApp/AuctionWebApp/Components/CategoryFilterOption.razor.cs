using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class CategoryFilterOption : ComponentBase
{
	[Parameter]
	public CategoryViewModel Category { get; set; } = new();
}
