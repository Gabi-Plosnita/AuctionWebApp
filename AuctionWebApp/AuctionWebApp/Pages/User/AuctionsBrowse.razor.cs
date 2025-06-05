using AuctionWebApp.Enums;
using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class AuctionsBrowse(IAuctionService AuctionService, 
									ICategoryService CategoryService,
									IAuthService AuthService) : ComponentBase
{
	private List<CategoryViewModel> categoryViewModels = new();

	private List<PreviewAuctionViewModel> previewAuctionViewModels = new();

	private AuctionFilterViewModel filterViewModel = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InProgress,
	};

	bool isLoading = true;

	bool showErrorComponent = false;

	protected override async Task OnInitializedAsync()
	{
		await SetExcludedSellerIdInAuctionFilterAsync();
		if (showErrorComponent || !isLoading)
			return;

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

	private async Task SetExcludedSellerIdInAuctionFilterAsync()
	{
		var authenticatedUserResult = await AuthService.GetAuthenticatedUserAsync();
		if (authenticatedUserResult.HasErrors || authenticatedUserResult.Data == null)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}
		filterViewModel.ExcludedSellerId = authenticatedUserResult.Data.Id;
	}

	private async Task HandleCategorySelectedAsync(int categoryId)
	{
		filterViewModel.CategoryId = categoryId;
		isLoading = true;
		showErrorComponent = false;
		var auctionsResult = await AuctionService.GetByFilterAsync(filterViewModel);
		if (auctionsResult.HasErrors || auctionsResult.Data == null)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}
		previewAuctionViewModels = auctionsResult.Data.Items.ToList();
		isLoading = false;
		StateHasChanged();
	}
}
