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
public class DriverServiceTests
{
	private Mock<IDriverHttpClient> _clientMock = default!;
	private Mock<IMapper> _mapperMock = default!;
	private DriverService _svc = default!;

	[TestInitialize]
	public void Setup()
	{
		_clientMock = new Mock<IDriverHttpClient>();
		_mapperMock = new Mock<IMapper>();
		_svc = new DriverService(_clientMock.Object, _mapperMock.Object);
	}

	[TestMethod]
	public async Task GetAllAsync_MapsDataAndPropagatesErrors()
	{
		var responses = new List<DriverResponse>
			{
				new() { Id = 1, Email = "a@x.com", Role = Role.Driver },
				new() { Id = 2, Email = "b@x.com", Role = Role.SuperAdmin }
			};
		var clientResult = new Result<List<DriverResponse>>
		{
			Data = responses,
			Errors = new List<string> { "E1", "E2" }
		};
		var vms = new List<DriverViewModel>
			{
				new() { Id = 1, Email = "a@x.com", Role = Role.Driver },
				new() { Id = 2, Email = "b@x.com", Role = Role.SuperAdmin }
			};

		_clientMock.Setup(c => c.GetAllAsync()).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<List<DriverViewModel>>(responses)).Returns(vms);

		var result = await _svc.GetAllAsync();

		CollectionAssert.AreEqual(vms, result.Data!);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetAllAsync_ClientReturnsErrors_DataNull()
	{
		var clientResult = new Result<List<DriverResponse>>
		{
			Data = null,
			Errors = new List<string> { "Server error" }
		};

		_clientMock.Setup(c => c.GetAllAsync()).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<List<DriverViewModel>>(null))
				   .Returns((List<DriverViewModel>?)null!);

		var result = await _svc.GetAllAsync();

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
		_mapperMock.Verify(m => m.Map<List<DriverViewModel>>(null), Times.Once);
	}

	[TestMethod]
	public async Task GetByIdAsync_MapsDataAndPropagatesErrors()
	{
		var response = new DriverResponse { Id = 42, Email = "z@x.com", Role = Role.Admin };
		var clientResult = new Result<DriverResponse>
		{
			Data = response,
			Errors = new List<string> { "nf" }
		};
		var vm = new DriverViewModel { Id = 42, Email = "z@x.com", Role = Role.Admin };

		_clientMock.Setup(c => c.GetByIdAsync(42)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<DriverViewModel>(response)).Returns(vm);

		var result = await _svc.GetByIdAsync(42);

		Assert.AreEqual(vm, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetByIdAsync_ClientReturnsErrors_DataNull()
	{
		var clientResult = new Result<DriverResponse>
		{
			Data = null,
			Errors = new List<string> { "NotFound" }
		};

		_clientMock.Setup(c => c.GetByIdAsync(5)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<DriverViewModel>(null))
				   .Returns((DriverViewModel?)null!);

		var result = await _svc.GetByIdAsync(5);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
		_mapperMock.Verify(m => m.Map<DriverViewModel>(null), Times.Once);
	}

	[TestMethod]
	public async Task DeleteByIdAsync_ForwardsResultDirectly()
	{
		var expected = new Result();
		_clientMock.Setup(c => c.DeleteByIdAsync(99)).ReturnsAsync(expected);

		var actual = await _svc.DeleteByIdAsync(99);

		Assert.AreSame(expected, actual);
	}

	[TestMethod]
	public async Task DeleteByIdAsync_ForwardsResultWithErrors()
	{
		var clientResult = new Result { Errors = new List<string> { "Unauthorized" } };
		_clientMock.Setup(c => c.DeleteByIdAsync(10)).ReturnsAsync(clientResult);

		var result = await _svc.DeleteByIdAsync(10);

		Assert.AreSame(clientResult, result);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}
}
