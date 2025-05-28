using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class CategoriesComponent : ComponentBase
{
	[Inject]
	public ICategoryService CategoryService { get; set; }

	
}
