using AuctionWebApp.Enums;
using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class AuctionsBrowse(IAuctionService AuctionService,
									ICategoryService CategoryService) : ComponentBase
{
	private List<CategoryViewModel> categoryViewModels = new();

	private List<PreviewAuctionViewModel> previewAuctionViewModels = new();

	private AuctionFilterViewModel filterViewModel = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InProgress,
	};

	private CategoryViewModel? selectedCategory = null;

	bool isLoading = true;

	bool showErrorComponent = false;

	protected override async Task OnInitializedAsync()
	{
		var categoriesResult = await InitializeCategoriesAsync();
		if (showErrorComponent || !isLoading)
			return;

		var auctionsResult = await InitializeAuctionsAsync();
		if (showErrorComponent || !isLoading)
			return;

		isLoading = false;
	}

	private async Task<Result> InitializeCategoriesAsync()
	{
		var categoriesResult = await CategoryService.GetAllAsync();
		if (categoriesResult.HasErrors || categoriesResult.Data == null)
		{
			showErrorComponent = true;
			isLoading = false;
			return categoriesResult;
		}
		categoryViewModels = categoriesResult.Data;

		return categoriesResult;
	}

	private async Task<Result> InitializeAuctionsAsync()
	{
		var auctionsResult = await AuctionService.GetByFilterAsync(filterViewModel);
		if (auctionsResult.HasErrors || auctionsResult.Data == null)
		{
			showErrorComponent = true;
			isLoading = false;
			return auctionsResult;
		}
		previewAuctionViewModels = auctionsResult.Data.Items.ToList();

		return auctionsResult;
	}
}
