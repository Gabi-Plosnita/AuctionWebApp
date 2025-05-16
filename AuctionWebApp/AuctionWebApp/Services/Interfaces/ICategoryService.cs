using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface ICategoryService
{
	Task<Result<List<CategoryResponseViewModel>>> GetAllAsync();

	Task<Result<CategoryResponseViewModel>> CreateAsync(CreateCategoryRequestViewModel request);

	Task<Result> UpdateAsync(int id, UpdateCategoryRequestViewModel request);
}
