using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public class AdminsHttpClient : BaseHttpClient, IAdminsHttpClient
{
	public AdminsHttpClient(HttpClient http) : base(http) { }	

	public async Task<Result<List<AdminResponse>>> GetAllAsync()
		=> await SendRequestAsync<List<AdminResponse>>("api/admins", HttpMethod.Get);

	public async Task<Result<AdminResponse>> GetByIdAsync(int id)
		=> await SendRequestAsync<AdminResponse>($"api/admins/{id}", HttpMethod.Get);

	public async Task<Result> DeleteByIdAsync(int id)
		=> await SendRequestAsync($"api/admins/{id}", HttpMethod.Delete);
}
