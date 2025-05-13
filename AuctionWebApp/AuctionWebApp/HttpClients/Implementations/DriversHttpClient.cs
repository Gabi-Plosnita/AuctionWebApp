using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public class DriversHttpClient : BaseHttpClient, IDriversHttpClient
{
	public DriversHttpClient(HttpClient http) : base(http) { }

	public async Task<Result<List<DriverResponse>>> GetAllAsync()
		=> await SendRequestAsync<List<DriverResponse>>("api/drivers", HttpMethod.Get);

	public async Task<Result<DriverResponse>> GetByIdAsync(int id)
		=> await SendRequestAsync<DriverResponse>($"api/drivers/{id}", HttpMethod.Get);

	public async Task<Result> DeleteByIdAsync(int id)
		=> await SendRequestAsync($"api/drivers/{id}", HttpMethod.Delete);
}
