using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface IDriverService
{
	Task<Result<List<DriverResponseViewModel>>> GetAllAsync();

	Task<Result<DriverResponseViewModel>> GetByIdAsync(int id);

	Task<Result> DeleteByIdAsync(int id);
}
