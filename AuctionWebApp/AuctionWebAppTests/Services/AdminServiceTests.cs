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
public class AdminServiceTests
{
	private Mock<IAdminHttpClient> _adminClientMock = default!;
	private Mock<IMapper> _mapperMock = default!;
	private AdminService _service = default!;

	[TestInitialize]
	public void Init()
	{
		_adminClientMock = new Mock<IAdminHttpClient>();
		_mapperMock = new Mock<IMapper>();
		_service = new AdminService(_adminClientMock.Object, _mapperMock.Object);
	}

	[TestMethod]
	public async Task GetAllAsync_MapsDataAndPropagatesErrors()
	{
		var responses = new List<AdminResponse>
			{
				new() { Id = 1, Email = "a@x.com", Role = Role.Admin },
				new() { Id = 2, Email = "b@x.com", Role = Role.User }
			};
		var clientResult = new Result<List<AdminResponse>>
		{
			Data = responses,
			Errors = new List<string> { "E1", "E2" }
		};
		var viewModels = new List<AdminViewModel>
			{
				new() { Id = 1, Email = "a@x.com", Role = Role.Admin },
				new() { Id = 2, Email = "b@x.com", Role = Role.User }
			};

		_adminClientMock
			.Setup(c => c.GetAllAsync())
			.ReturnsAsync(clientResult);

		_mapperMock
			.Setup(m => m.Map<List<AdminViewModel>>(responses))
			.Returns(viewModels);

		var result = await _service.GetAllAsync();

		CollectionAssert.AreEqual(viewModels, result.Data!);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetByIdAsync_MapsDataAndPropagatesErrors()
	{
		var response = new AdminResponse { Id = 42, Email = "z@x.com", Role = Role.SuperAdmin };
		var clientResult = new Result<AdminResponse>
		{
			Data = response,
			Errors = new List<string> { "NotFound" }
		};
		var vm = new AdminViewModel { Id = 42, Email = "z@x.com", Role = Role.SuperAdmin };

		_adminClientMock
			.Setup(c => c.GetByIdAsync(42))
			.ReturnsAsync(clientResult);

		_mapperMock
			.Setup(m => m.Map<AdminViewModel>(response))
			.Returns(vm);

		var result = await _service.GetByIdAsync(42);

		Assert.AreEqual(vm, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task DeleteByIdAsync_ForwardsResultDirectly()
	{
		var expected = new Result();
		_adminClientMock
			.Setup(c => c.DeleteByIdAsync(99))
			.ReturnsAsync(expected);

		var actual = await _service.DeleteByIdAsync(99);

		Assert.AreSame(expected, actual);
	}
}
