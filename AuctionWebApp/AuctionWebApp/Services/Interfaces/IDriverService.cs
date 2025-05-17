using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface IDriverService
{
	Task<Result<List<DriverViewModel>>> GetAllAsync();

	Task<Result<DriverViewModel>> GetByIdAsync(int id);

	Task<Result> DeleteByIdAsync(int id);
}
