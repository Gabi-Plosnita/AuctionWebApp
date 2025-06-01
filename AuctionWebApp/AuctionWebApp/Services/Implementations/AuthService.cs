using AuctionWebApp.Helpers;
using AuctionWebApp.HttpClients;
using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Services;

public class AuthService(IAuthHttpClient _authClient, IMapper _mapper) : IAuthService
{
	public async Task<Result<AdminViewModel>> RegisterAdminAsync(RegisterAdminViewModel registerAdminViewModel)
	{
		var registerAdminRequest = _mapper.Map<RegisterAdminRequest>(registerAdminViewModel);
		var clientResult = await _authClient.RegisterAdminAsync(registerAdminRequest);
		var adminViewModel = _mapper.Map<AdminViewModel>(clientResult.Data);
		var serviceResult = new Result<AdminViewModel>
		{
			Data = adminViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result> LoginAdminAsync(LoginViewModel loginViewModel)
	{
		var loginRequest = _mapper.Map<LoginRequest>(loginViewModel);
		var clientResult = await _authClient.LoginAdminAsync(loginRequest);
		return clientResult;
	}

	public async Task<Result<DriverViewModel>> RegisterDriverAsync(RegisterDriverViewModel registerDriverViewModel)
	{
		var registerDriverRequest = _mapper.Map<RegisterDriverRequest>(registerDriverViewModel);
		var clientResult = await _authClient.RegisterDriverAsync(registerDriverRequest);
		var driverViewModel = _mapper.Map<DriverViewModel>(clientResult.Data);
		var serviceResult = new Result<DriverViewModel>
		{
			Data = driverViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result> LoginDriverAsync(LoginViewModel loginViewModel)
	{
		var loginRequest = _mapper.Map<LoginRequest>(loginViewModel);
		var clientResult = await _authClient.LoginDriverAsync(loginRequest);
		return clientResult;
	}
	public async Task<Result<UserViewModel>> RegisterUserAsync(RegisterUserViewModel registerUserViewModel)
	{
		var registerUserRequest = _mapper.Map<RegisterUserRequest>(registerUserViewModel);
		var clientResult = await _authClient.RegisterUserAsync(registerUserRequest);
		var userViewModel = _mapper.Map<UserViewModel>(clientResult.Data);
		var serviceResult = new Result<UserViewModel>
		{
			Data = userViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}	

	public async Task<Result> LoginUserAsync(LoginViewModel loginViewModel)
	{
		var loginRequest = _mapper.Map<LoginRequest>(loginViewModel);
		var clientResult = await _authClient.LoginUserAsync(loginRequest);
		return clientResult;
	}

	public async Task<Result<AuthenticatedUserViewModel>> GetAuthenticatedUserAsync()
	{
		var clientResult = await _authClient.GetAuthenticatedUserResponseAsync();
		var authenticatedUserViewModel = _mapper.Map<AuthenticatedUserViewModel>(clientResult.Data);
		var serviceResult = new Result<AuthenticatedUserViewModel>
		{
			Data = authenticatedUserViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result> LogoutAsync()
	{
		return await _authClient.LogoutAsync();
	}
}
