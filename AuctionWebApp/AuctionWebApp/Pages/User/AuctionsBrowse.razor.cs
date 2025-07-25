﻿using AuctionWebApp.Enums;
using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class AuctionsBrowse(IAuctionService AuctionService, 
									ICategoryService CategoryService,
									IAuthService AuthService) : ComponentBase
{
	private List<CategoryViewModel> categoryViewModels = new();

	private List<PreviewAuctionViewModel> previewAuctionViewModels = new();

	private AuctionFilterViewModel filterViewModel = new AuctionFilterViewModel
	{
		PageSize = 12,
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
		totalPages = auctionsResult.Data.TotalPages;

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
		ResetPagination();
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
		totalPages = auctionsResult.Data.TotalPages;
		isLoading = false;
		StateHasChanged();
	}

	// Pagination //

	private int totalPages = 1;

	private bool CanGoToPreviousPage => filterViewModel.Page > 1;
	private bool CanGoToNextPage => filterViewModel.Page < totalPages;

	private async Task NextPage()
	{
		if (!CanGoToNextPage) return;

		filterViewModel.Page++;
		await LoadPagedAuctionsAsync();
	}

	private async Task PreviousPage()
	{
		if (!CanGoToPreviousPage) return;

		filterViewModel.Page--;
		await LoadPagedAuctionsAsync();
	}

	private async Task LoadPagedAuctionsAsync()
	{
		isLoading = true;
		showErrorComponent = false;

		var result = await AuctionService.GetByFilterAsync(filterViewModel);
		if (result.HasErrors || result.Data == null)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}

		previewAuctionViewModels = result.Data.Items.ToList();
		totalPages = result.Data.TotalPages;
		isLoading = false;
		StateHasChanged();
	}
	private void ResetPagination()
	{
		filterViewModel.Page = 1;
		totalPages = 1;
	}
}
