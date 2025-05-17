using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public interface ICategoryHttpClient
{
	Task<Result<List<CategoryResponse>>> GetAllAsync();

	Task<Result<CategoryResponse>> CreateAsync(CreateCategoryRequest createCategoryRequest);

	Task<Result> UpdateAsync(int id, UpdateCategoryRequest updateCategoryRequest);
}
