using AuctionWebApp.Helpers;
using AuctionWebApp.HttpClients;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Services;

public class DriverService(IDriverHttpClient _driverClient, IMapper _mapper) : IDriverService
{
	public async Task<Result<List<DriverViewModel>>> GetAllAsync()
	{
		var clientResult = await _driverClient.GetAllAsync();
		var driverViewModels = _mapper.Map<List<DriverViewModel>>(clientResult.Data);
		var serviceResult = new Result<List<DriverViewModel>>
		{
			Data = driverViewModels,
			Errors = clientResult.Errors,
		};
		return serviceResult;
	}

	public async Task<Result<DriverViewModel>> GetByIdAsync(int id)
	{
		var clientResult = await _driverClient.GetByIdAsync(id);
		var driverViewModel = _mapper.Map<DriverViewModel>(clientResult.Data);
		var serviceResult = new Result<DriverViewModel>
		{
			Data = driverViewModel,
			Errors = clientResult.Errors,
		};
		return serviceResult;
	}

	public async Task<Result> DeleteByIdAsync(int id)
	{
		return await _driverClient.DeleteByIdAsync(id);
	}
}
