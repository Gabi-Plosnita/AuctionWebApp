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
public class UserServiceTests
{
	private Mock<IUserHttpClient> _clientMock = default!;
	private Mock<IMapper> _mapperMock = default!;
	private UserService _svc = default!;

	[TestInitialize]
	public void Setup()
	{
		_clientMock = new Mock<IUserHttpClient>();
		_mapperMock = new Mock<IMapper>();
		_svc = new UserService(_clientMock.Object, _mapperMock.Object);
	}

	[TestMethod]
	public async Task GetByIdAsync_MapsDataAndPropagatesErrors()
	{
		var response = new UserResponse { Id = 10, Role = Role.User, Email = "u@x.com", FirstName = "F", LastName = "L", Address = "A" };
		var clientResult = new Result<UserResponse> { Data = response, Errors = new List<string> { "E" } };
		var vm = new UserViewModel { Id = 10, Role = Role.User, Email = "u@x.com", FirstName = "F", LastName = "L", Address = "A" };

		_clientMock.Setup(c => c.GetByIdAsync(10)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<UserViewModel>(response)).Returns(vm);

		var result = await _svc.GetByIdAsync(10);

		Assert.AreEqual(vm, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetByIdAsync_ClientReturnsErrors_DataNull()
	{
		var clientResult = new Result<UserResponse> { Data = null, Errors = new List<string> { "NotFound" } };

		_clientMock.Setup(c => c.GetByIdAsync(5)).ReturnsAsync(clientResult);

		var result = await _svc.GetByIdAsync(5);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task UpdateAsync_ForwardsClientResult_Success()
	{
		var vmReq = new UpdateUserViewModel { FirstName = "X", LastName = "Y", Address = "Z" };
		var req = new UpdateUserRequest { FirstName = "X", LastName = "Y", Address = "Z" };
		var clientResult = new Result();

		_mapperMock.Setup(m => m.Map<UpdateUserRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.UpdateAsync(3, req)).ReturnsAsync(clientResult);

		var result = await _svc.UpdateAsync(3, vmReq);

		Assert.AreSame(clientResult, result);
	}

	[TestMethod]
	public async Task UpdateAsync_ForwardsClientResult_Errors()
	{
		var vmReq = new UpdateUserViewModel();
		var req = new UpdateUserRequest();
		var clientResult = new Result { Errors = new List<string> { "Bad" } };

		_mapperMock.Setup(m => m.Map<UpdateUserRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.UpdateAsync(7, req)).ReturnsAsync(clientResult);

		var result = await _svc.UpdateAsync(7, vmReq);

		Assert.IsTrue(result.HasErrors);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task CreateConnectedAccountAsync_ForwardsClientResult_Success()
	{
		var vmReq = new CreateConnectedStripeAccountViewModel { AccountType = StripeAccountType.Standard, Email = "e", Country = "C", Currency = "CUR" };
		var req = new CreateConnectedStripeAccountRequest { AccountType = StripeAccountType.Standard, Email = "e", Country = "C", Currency = "CUR" };
		var clientResult = new Result<string> { Data = "acct_123", Errors = new List<string>() };

		_mapperMock.Setup(m => m.Map<CreateConnectedStripeAccountRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.CreateConnectedAccountAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.CreateConnectedAccountAsync(vmReq);

		Assert.AreSame(clientResult, result);
	}

	[TestMethod]
	public async Task CreateConnectedAccountAsync_ForwardsClientResult_Errors()
	{
		var vmReq = new CreateConnectedStripeAccountViewModel();
		var req = new CreateConnectedStripeAccountRequest();
		var clientResult = new Result<string> { Data = null, Errors = new List<string> { "Err" } };

		_mapperMock.Setup(m => m.Map<CreateConnectedStripeAccountRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.CreateConnectedAccountAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.CreateConnectedAccountAsync(vmReq);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetAccountLinkAsync_ForwardsClientResult_Success()
	{
		var vmReq = new CreateAccountLinkViewModel { ConnectedAccountId = "id", RefreshUrl = "r", ReturnUrl = "t", LinkType = StripeLinkType.AccountOnboarding };
		var req = new CreateAccountLinkRequest { ConnectedAccountId = "id", RefreshUrl = "r", ReturnUrl = "t", LinkType = StripeLinkType.AccountOnboarding };
		var clientResult = new Result<string> { Data = "link", Errors = new List<string>() };

		_mapperMock.Setup(m => m.Map<CreateAccountLinkRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.GetAccountLinkAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.GetAccountLinkAsync(vmReq);

		Assert.AreSame(clientResult, result);
	}

	[TestMethod]
	public async Task GetAccountLinkAsync_ForwardsClientResult_Errors()
	{
		var vmReq = new CreateAccountLinkViewModel();
		var req = new CreateAccountLinkRequest();
		var clientResult = new Result<string> { Data = null, Errors = new List<string> { "E" } };

		_mapperMock.Setup(m => m.Map<CreateAccountLinkRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.GetAccountLinkAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.GetAccountLinkAsync(vmReq);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task CreateCustomerAccountAsync_ForwardsClientResult_Success()
	{
		var vmReq = new CreateStripeCustomerAccountViewModel { PaymentMethodId = "pm" };
		var req = new CreateStripeCustomerAccountRequest { PaymentMethodId = "pm" };
		var clientResult = new Result<string> { Data = "cust_1", Errors = new List<string>() };

		_mapperMock.Setup(m => m.Map<CreateStripeCustomerAccountRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.CreateCustomerAccountAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.CreateCustomerAccountAsync(vmReq);

		Assert.AreSame(clientResult, result);
	}

	[TestMethod]
	public async Task CreateCustomerAccountAsync_ForwardsClientResult_Errors()
	{
		var vmReq = new CreateStripeCustomerAccountViewModel();
		var req = new CreateStripeCustomerAccountRequest();
		var clientResult = new Result<string> { Data = null, Errors = new List<string> { "Err" } };

		_mapperMock.Setup(m => m.Map<CreateStripeCustomerAccountRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.CreateCustomerAccountAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.CreateCustomerAccountAsync(vmReq);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task UpdatePaymentMethodAsync_ForwardsClientResult_Success()
	{
		var vmReq = new UpdateStripePaymentMethodViewModel { PaymentMethodId = "pm" };
		var req = new UpdateStripePaymentMethodRequest { PaymentMethodId = "pm" };
		var clientResult = new Result();

		_mapperMock.Setup(m => m.Map<UpdateStripePaymentMethodRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.UpdatePaymentMethodAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.UpdatePaymentMethodAsync(vmReq);

		Assert.AreSame(clientResult, result);
	}

	[TestMethod]
	public async Task UpdatePaymentMethodAsync_ForwardsClientResult_Errors()
	{
		var vmReq = new UpdateStripePaymentMethodViewModel();
		var req = new UpdateStripePaymentMethodRequest() {PaymentMethodId = "invalid"};
		var clientResult = new Result { Errors = new List<string> { "Bad" } };

		_mapperMock.Setup(m => m.Map<UpdateStripePaymentMethodRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.UpdatePaymentMethodAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.UpdatePaymentMethodAsync(vmReq);

		Assert.IsTrue(result.HasErrors);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetStripeConnectedAccountDetailsAsync_MapsDataAndPropagatesErrors()
	{
		var resp = new StripeConnectedAccountResponse
		{
			AccountId = "acc",
			BusinessName = "B",
			Email = "e",
			Country = "C",
			ChargesEnabled = true,
			PayoutsEnabled = false,
			DetailsSubmitted = true
		};
		var clientResult = new Result<StripeConnectedAccountResponse> { Data = resp, Errors = new List<string> { "E" } };
		var vm = new StripeConnectedAccountViewModel { AccountId = "acc", BusinessName = "B", Email = "e", Country = "C", ChargesEnabled = true, PayoutsEnabled = false, DetailsSubmitted = true };

		_clientMock.Setup(c => c.GetStripeConnectedAccountDetails()).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<StripeConnectedAccountViewModel>(resp)).Returns(vm);

		var result = await _svc.GetStripeConnectedAccountDetailsAsync();

		Assert.AreEqual(vm, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetStripeConnectedAccountDetailsAsync_ClientReturnsErrors_DataNull()
	{
		var clientResult = new Result<StripeConnectedAccountResponse> { Data = null, Errors = new List<string> { "Err" } };

		_clientMock.Setup(c => c.GetStripeConnectedAccountDetails()).ReturnsAsync(clientResult);

		var result = await _svc.GetStripeConnectedAccountDetailsAsync();

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetStripePaymentMethodAsync_MapsDataAndPropagatesErrors()
	{
		var resp = new StripePaymentMethodResponse { Brand = "V", Last4 = "1234", ExpMonth = "01", ExpYear = "25" };
		var clientResult = new Result<StripePaymentMethodResponse> { Data = resp, Errors = new List<string> { "E" } };
		var vm = new StripePaymentMethodViewModel { Brand = "V", Last4 = "1234", ExpMonth = "01", ExpYear = "25" };

		_clientMock.Setup(c => c.GetStripePaymentMethodAsync()).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<StripePaymentMethodViewModel>(resp)).Returns(vm);

		var result = await _svc.GetStripePaymentMethodAsync();

		Assert.AreEqual(vm, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetStripePaymentMethodAsync_ClientReturnsErrors_DataNull()
	{
		var clientResult = new Result<StripePaymentMethodResponse> { Data = null, Errors = new List<string> { "Err" } };

		_clientMock.Setup(c => c.GetStripePaymentMethodAsync()).ReturnsAsync(clientResult);

		var result = await _svc.GetStripePaymentMethodAsync();

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}
}