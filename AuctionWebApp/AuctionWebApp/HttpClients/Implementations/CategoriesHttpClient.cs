using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public class CategoriesHttpClient : BaseHttpClient, ICategoriesHttpClient
{
	public CategoriesHttpClient(HttpClient http) : base(http) { }

	public async Task<Result<List<CategoryResponse>>> GetAllAsync()
		=> await SendRequestAsync<List<CategoryResponse>>("api/categories", HttpMethod.Get);

	public async Task<Result<CategoryResponse>> CreateAsync(CreateCategoryRequest request)
		=> await SendRequestAsync<CategoryResponse>("api/categories", HttpMethod.Post, request);

	public async Task<Result> UpdateAsync(int id, UpdateCategoryRequest request)
		=> await SendRequestAsync($"api/categories/{id}", HttpMethod.Put, request);
}
