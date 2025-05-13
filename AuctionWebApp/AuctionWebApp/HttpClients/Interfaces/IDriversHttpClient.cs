using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public interface IDriversHttpClient
{
	Task<Result<List<DriverResponse>>> GetAllAsync();

	Task<Result<DriverResponse>> GetByIdAsync(int id);

	Task<Result> DeleteByIdAsync(int id);
}
