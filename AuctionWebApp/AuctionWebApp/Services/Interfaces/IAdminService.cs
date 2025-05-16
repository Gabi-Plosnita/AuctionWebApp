using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface IAdminService
{
	Task<Result<List<AdminViewModel>>> GetAllAsync();

	Task<Result<AdminViewModel>> GetByIdAsync(int id);

	Task<Result> DeleteByIdAsync(int id);
}
