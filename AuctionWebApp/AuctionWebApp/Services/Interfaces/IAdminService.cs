using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface IAdminService
{
	Task<Result<List<AdminResponseViewModel>>> GetAllAsync();

	Task<Result<AdminResponseViewModel>> GetByIdAsync(int id);

	Task<Result> DeleteByIdAsync(int id);
}
