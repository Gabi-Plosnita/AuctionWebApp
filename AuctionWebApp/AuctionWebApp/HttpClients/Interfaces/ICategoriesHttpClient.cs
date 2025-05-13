using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public interface ICategoriesHttpClient
{
	Task<Result<List<CategoryResponse>>> GetAllAsync();

	Task<Result<CategoryResponse>> CreateAsync(CreateCategoryRequest request);

	Task<Result> UpdateAsync(int id, UpdateCategoryRequest request);
}
