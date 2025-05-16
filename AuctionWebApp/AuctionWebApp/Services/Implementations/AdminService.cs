using AuctionWebApp.Helpers;
using AuctionWebApp.HttpClients;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Services;

public class AdminService(IAdminHttpClient _adminClient, IMapper _mapper) : IAdminService
{
	public async Task<Result<List<AdminViewModel>>> GetAllAsync()
	{
		var clientResult = await _adminClient.GetAllAsync();
		var adminViewModels = _mapper.Map<List<AdminViewModel>>(clientResult.Data);
		var serviceResult = new Result<List<AdminViewModel>>
		{
			Data = adminViewModels,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result<AdminViewModel>> GetByIdAsync(int id)
	{
		var clientResult = await _adminClient.GetByIdAsync(id);
		var adminViewModel = _mapper.Map<AdminViewModel>(clientResult.Data);
		var serviceResult = new Result<AdminViewModel>
		{
			Data = adminViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result> DeleteByIdAsync(int id)
	{
		return await _adminClient.DeleteByIdAsync(id);
	}
}
