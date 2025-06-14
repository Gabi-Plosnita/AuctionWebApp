using AuctionWebApp.Enums;
using AuctionWebApp.Helpers;
using AuctionWebApp.HttpClients;
using AuctionWebApp.Models;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using AutoMapper;
using Moq;

namespace AuctionWebAppTests.Services;

[TestClass]
public class AuthServiceTests
{
	private Mock<IAuthHttpClient> _clientMock = default!;
	private Mock<IMapper> _mapperMock = default!;
	private AuthService _svc = default!;

	[TestInitialize]
	public void Setup()
	{
		_clientMock = new Mock<IAuthHttpClient>();
		_mapperMock = new Mock<IMapper>();
		_svc = new AuthService(_clientMock.Object, _mapperMock.Object);
	}

	[TestMethod]
	public async Task RegisterAdminAsync_MapsRequest_MapsResult()
	{
		var vm = new RegisterAdminViewModel();
		var req = new RegisterAdminRequest();
		var resp = new AdminResponse { Id = 1, Email = "a@x.com", Role = Role.Admin };
		var clientResult = new Result<AdminResponse> { Data = resp, Errors = new List<string> { "E" } };
		var vmOut = new AdminViewModel { Id = 1, Email = "a@x.com", Role = Role.Admin };

		_mapperMock.Setup(m => m.Map<RegisterAdminRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.RegisterAdminAsync(req)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<AdminViewModel>(resp)).Returns(vmOut);

		var result = await _svc.RegisterAdminAsync(vm);

		Assert.AreEqual(vmOut, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task RegisterAdminAsync_ClientReturnsErrors_DataNull()
	{
		var vm = new RegisterAdminViewModel();
		var req = new RegisterAdminRequest();
		var clientResult = new Result<AdminResponse> { Data = null, Errors = new List<string> { "Err" } };

		_mapperMock.Setup(m => m.Map<RegisterAdminRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.RegisterAdminAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.RegisterAdminAsync(vm);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task LoginAdminAsync_ForwardsClientResult_Success()
	{
		var vm = new LoginViewModel();
		var req = new LoginRequest();
		var clientResult = new Result { };

		_mapperMock.Setup(m => m.Map<LoginRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.LoginAdminAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.LoginAdminAsync(vm);

		Assert.AreSame(clientResult, result);
	}

	[TestMethod]
	public async Task LoginAdminAsync_ForwardsClientResult_Errors()
	{
		var vm = new LoginViewModel();
		var req = new LoginRequest();
		var clientResult = new Result { Errors = new List<string> { "Bad" } };

		_mapperMock.Setup(m => m.Map<LoginRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.LoginAdminAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.LoginAdminAsync(vm);

		Assert.AreSame(clientResult, result);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task RegisterDriverAsync_MapsRequest_MapsResult()
	{
		var vm = new RegisterDriverViewModel();
		var req = new RegisterDriverRequest();
		var resp = new DriverResponse { Id = 2, Email = "driver@gmail.com", Role = Role.Driver};
		var clientResult = new Result<DriverResponse> { Data = resp, Errors = new List<string> { "X" } };
		var vmOut = new DriverViewModel { Id = 2, Email = "driver@gmail.com", Role = Role.Driver };

		_mapperMock.Setup(m => m.Map<RegisterDriverRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.RegisterDriverAsync(req)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<DriverViewModel>(resp)).Returns(vmOut);

		var result = await _svc.RegisterDriverAsync(vm);

		Assert.AreEqual(vmOut, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task RegisterDriverAsync_ClientReturnsErrors_DataNull()
	{
		var vm = new RegisterDriverViewModel();
		var req = new RegisterDriverRequest();
		var clientResult = new Result<DriverResponse> { Data = null, Errors = new List<string> { "Y" } };

		_mapperMock.Setup(m => m.Map<RegisterDriverRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.RegisterDriverAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.RegisterDriverAsync(vm);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task LoginDriverAsync_ForwardsClientResult()
	{
		var vm = new LoginViewModel();
		var req = new LoginRequest();
		var clientResult = new Result { Errors = new List<string> { "Z" } };

		_mapperMock.Setup(m => m.Map<LoginRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.LoginDriverAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.LoginDriverAsync(vm);

		Assert.AreSame(clientResult, result);
	}

	[TestMethod]
	public async Task RegisterUserAsync_MapsRequest_MapsResult()
	{
		var vm = new RegisterUserViewModel();
		var req = new RegisterUserRequest();
		var resp = new UserResponse { Id = 3, Email = "u@x.com" };
		var clientResult = new Result<UserResponse> { Data = resp, Errors = new List<string> { "U" } };
		var vmOut = new UserViewModel { Id = 3, Email = "u@x.com" };

		_mapperMock.Setup(m => m.Map<RegisterUserRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.RegisterUserAsync(req)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<UserViewModel>(resp)).Returns(vmOut);

		var result = await _svc.RegisterUserAsync(vm);

		Assert.AreEqual(vmOut, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task RegisterUserAsync_ClientReturnsErrors_DataNull()
	{
		var vm = new RegisterUserViewModel();
		var req = new RegisterUserRequest();
		var clientResult = new Result<UserResponse> { Data = null, Errors = new List<string> { "V" } };

		_mapperMock.Setup(m => m.Map<RegisterUserRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.RegisterUserAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.RegisterUserAsync(vm);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task LoginUserAsync_ForwardsClientResult()
	{
		var vm = new LoginViewModel();
		var req = new LoginRequest();
		var clientResult = new Result { };

		_mapperMock.Setup(m => m.Map<LoginRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.LoginUserAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.LoginUserAsync(vm);

		Assert.AreSame(clientResult, result);
	}

	[TestMethod]
	public async Task RequestPasswordResetAsync_ForwardsClientResult()
	{
		var vm = new PasswordResetViewModel();
		var req = new PasswordResetRequest();
		var clientResult = new Result { Errors = new List<string> { "P" } };

		_mapperMock.Setup(m => m.Map<PasswordResetRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.RequestPasswordResetAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.RequestPasswordResetAsync(vm);

		Assert.AreSame(clientResult, result);
	}

	[TestMethod]
	public async Task ResetPasswordAsync_ForwardsClientResult()
	{
		var vm = new ResetPasswordViewModel();
		var req = new ResetPasswordRequest();
		var clientResult = new Result { Errors = new List<string> { "R" } };

		_mapperMock.Setup(m => m.Map<ResetPasswordRequest>(vm)).Returns(req);
		_clientMock.Setup(c => c.ResetPassworAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.ResetPasswordAsync(vm);

		Assert.AreSame(clientResult, result);
	}

	[TestMethod]
	public async Task GetAuthenticatedUserAsync_MapsAndPropagates()
	{
		var resp = new AuthenticatedUserResponse { Id = 4, Email = "user@gmail.com", Role = "User" };
		var clientResult = new Result<AuthenticatedUserResponse> { Data = resp, Errors = new List<string> { "A" } };
		var vmOut = new AuthenticatedUserViewModel { Id = 4, Email = "user@gmail.com", Role = "User" };

		_clientMock.Setup(c => c.GetAuthenticatedUserResponseAsync()).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<AuthenticatedUserViewModel>(resp)).Returns(vmOut);

		var result = await _svc.GetAuthenticatedUserAsync();

		Assert.AreEqual(vmOut, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetAuthenticatedUserAsync_ClientReturnsErrors_DataNull()
	{
		var clientResult = new Result<AuthenticatedUserResponse> { Data = null, Errors = new List<string> { "NA" } };

		_clientMock.Setup(c => c.GetAuthenticatedUserResponseAsync()).ReturnsAsync(clientResult);

		var result = await _svc.GetAuthenticatedUserAsync();

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task LogoutAsync_ForwardsClientResult()
	{
		var clientResult = new Result { Errors = new List<string> { "L" } };

		_clientMock.Setup(c => c.LogoutAsync()).ReturnsAsync(clientResult);

		var result = await _svc.LogoutAsync();

		Assert.AreSame(clientResult, result);
	}
}
