using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public interface IAdminsHttpClient
{
	Task<Result<List<AdminResponse>>> GetAllAsync();

	Task<Result<AdminResponse>> GetByIdAsync(int id);

	Task<Result> DeleteByIdAsync(int id);
}
