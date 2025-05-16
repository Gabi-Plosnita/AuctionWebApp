using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface ICategoryService
{
	Task<Result<List<CategoryViewModel>>> GetAllAsync();

	Task<Result<CategoryViewModel>> CreateAsync(CreateCategoryViewModel createCategoryViewModel);

	Task<Result> UpdateAsync(int id, UpdateCategoryViewModel updateCategoryViewModel);
}
