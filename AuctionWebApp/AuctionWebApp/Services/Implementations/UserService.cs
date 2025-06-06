using AuctionWebApp.Helpers;
using AuctionWebApp.HttpClients;
using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Services;

public class UserService(IUserHttpClient _userClient, IMapper _mapper) : IUserService
{
	public async Task<Result<UserViewModel>> GetByIdAsync(int id)
	{
		var clientResult = await _userClient.GetByIdAsync(id);
		var userViewModel = _mapper.Map<UserViewModel>(clientResult.Data);
		var serviceResult = new Result<UserViewModel>
		{
			Data = userViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result> UpdateAsync(int id, UpdateUserViewModel updateUserViewModel)
	{
		var updateUserRequest = _mapper.Map<UpdateUserRequest>(updateUserViewModel);
		var clientResult = await _userClient.UpdateAsync(id, updateUserRequest);
		return clientResult;
	}
	public async Task<Result<string>> CreateConnectedAccountAsync(CreateConnectedStripeAccountViewModel createConnectedAccountViewModel)
	{
		var createConnectedAccountRequest = _mapper.Map<CreateConnectedStripeAccountRequest>(createConnectedAccountViewModel);
		var clientResult = await _userClient.CreateConnectedAccountAsync(createConnectedAccountRequest);
		return clientResult;
	}

	public async Task<Result<string>> GetAccountLinkAsync(CreateAccountLinkViewModel createLinkViewModel)
	{
		var createLinkRequest = _mapper.Map<CreateAccountLinkRequest>(createLinkViewModel);
		var clientResult = await _userClient.GetAccountLinkAsync(createLinkRequest);
		return clientResult;
	}

	public async Task<Result<string>> CreateCustomerAccountAsync(CreateStripeCustomerAccountViewModel createCustomerAccountViewModel)
	{
		var createCustomerAccountRequest = _mapper.Map<CreateStripeCustomerAccountRequest>(createCustomerAccountViewModel);
		var clientResult = await _userClient.CreateCustomerAccountAsync(createCustomerAccountRequest);
		return clientResult;
	}

	public async Task<Result<StripeConnectedAccountViewModel>> GetStripeConnectedAccountDetailsAsync()
	{
		var clientResult = await _userClient.GetStripeConnectedAccountDetails();
		var stripeConnectedAccountViewModel = _mapper.Map<StripeConnectedAccountViewModel>(clientResult.Data);
		var serviceResult = new Result<StripeConnectedAccountViewModel>
		{
			Data = stripeConnectedAccountViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}
}
