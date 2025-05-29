using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public class CategoryHttpClient : BaseHttpClient, ICategoryHttpClient
{
	public CategoryHttpClient(HttpClient http) : base(http) { }

	public async Task<Result<List<CategoryResponse>>> GetAllAsync()
		=> await SendRequestAsync<List<CategoryResponse>>("api/categories", HttpMethod.Get);

	public async Task<Result<CategoryResponse>> GetByIdAsync(int id)
		=> await SendRequestAsync<CategoryResponse>($"api/categories/{id}", HttpMethod.Get);

	public async Task<Result<CategoryResponse>> CreateAsync(CreateCategoryRequest createCategoryRequest)
		=> await SendFormRequestAsync<CategoryResponse>("api/categories", HttpMethod.Post, createCategoryRequest);

	public async Task<Result> UpdateAsync(int id, UpdateCategoryRequest updateCategoryRequest)
		=> await SendFormRequestAsync($"api/categories/{id}", HttpMethod.Put, updateCategoryRequest);
}
