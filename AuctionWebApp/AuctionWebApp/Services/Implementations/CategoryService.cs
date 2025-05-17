using AuctionWebApp.Helpers;
using AuctionWebApp.HttpClients;
using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Services;

public class CategoryService(ICategoryHttpClient _categoryClient, IMapper _mapper) : ICategoryService
{
	public async Task<Result<List<CategoryViewModel>>> GetAllAsync()
	{
		var clientResult = await _categoryClient.GetAllAsync();
		var categoryViewModels = _mapper.Map<List<CategoryViewModel>>(clientResult.Data);
		var serviceResult = new Result<List<CategoryViewModel>>
		{
			Data = categoryViewModels,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result<CategoryViewModel>> CreateAsync(CreateCategoryViewModel createCategoryViewModel)
	{
		var createCategoryRequest = _mapper.Map<CreateCategoryRequest>(createCategoryViewModel);
		var clientResult = await _categoryClient.CreateAsync(createCategoryRequest);
		var categoryViewModel = _mapper.Map<CategoryViewModel>(clientResult.Data);
		var serviceResult = new Result<CategoryViewModel>
		{
			Data = categoryViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result> UpdateAsync(int id, UpdateCategoryViewModel updateCategoryViewModel)
	{
		var updateCategoryRequest = _mapper.Map<UpdateCategoryRequest>(updateCategoryViewModel);
		var clientResult = await _categoryClient.UpdateAsync(id, updateCategoryRequest);
		return clientResult;
	}
}
