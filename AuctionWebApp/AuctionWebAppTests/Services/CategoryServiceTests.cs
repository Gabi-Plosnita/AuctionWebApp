using AuctionWebApp.Helpers;
using AuctionWebApp.HttpClients;
using AuctionWebApp.Models;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using AutoMapper;
using Moq;

namespace AuctionWebAppTests.Services;

[TestClass]
public class CategoryServiceTests
{
	private Mock<ICategoryHttpClient> _clientMock = default!;
	private Mock<IMapper> _mapperMock = default!;
	private CategoryService _svc = default!;

	[TestInitialize]
	public void Setup()
	{
		_clientMock = new Mock<ICategoryHttpClient>();
		_mapperMock = new Mock<IMapper>();
		_svc = new CategoryService(_clientMock.Object, _mapperMock.Object);
	}

	[TestMethod]
	public async Task GetAllAsync_MapsDataAndPropagatesErrors()
	{
		var responses = new List<CategoryResponse>
			{
				new() { Id = 1, Name = "A", ImageUrl = "u1" },
				new() { Id = 2, Name = "B", ImageUrl = null }
			};
		var clientResult = new Result<List<CategoryResponse>>
		{
			Data = responses,
			Errors = new List<string> { "E1" }
		};
		var vms = new List<CategoryViewModel>
			{
				new() { Id = 1, Name = "A", ImageUrl = "u1" },
				new() { Id = 2, Name = "B", ImageUrl = null }
			};

		_clientMock.Setup(c => c.GetAllAsync()).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<List<CategoryViewModel>>(responses)).Returns(vms);

		var result = await _svc.GetAllAsync();

		CollectionAssert.AreEqual(vms, result.Data!);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetAllAsync_ClientReturnsErrors_DataNull_ErrorsPropagated()
	{
		var clientResult = new Result<List<CategoryResponse>>
		{
			Data = null,
			Errors = new List<string> { "Err" }
		};

		_clientMock.Setup(c => c.GetAllAsync()).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<List<CategoryViewModel>>(null)).Returns((List<CategoryViewModel>?)null!);

		var result = await _svc.GetAllAsync();

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetByIdAsync_MapsOnSuccess()
	{
		var response = new CategoryResponse { Id = 5, Name = "Cat", ImageUrl = "u" };
		var clientResult = new Result<CategoryResponse>
		{
			Data = response,
			Errors = new List<string>()
		};
		var vm = new CategoryViewModel { Id = 5, Name = "Cat", ImageUrl = "u" };

		_clientMock.Setup(c => c.GetByIdAsync(5)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<CategoryViewModel>(response)).Returns(vm);

		var result = await _svc.GetByIdAsync(5);

		Assert.AreEqual(vm, result.Data);
		CollectionAssert.AreEqual(new List<string>(), result.Errors);
	}

	[TestMethod]
	public async Task GetByIdAsync_ClientReturnsErrors_NoMapping_DataNull()
	{
		var clientResult = new Result<CategoryResponse>
		{
			Data = null,
			Errors = new List<string> { "NotFound" }
		};

		_clientMock.Setup(c => c.GetByIdAsync(7)).ReturnsAsync(clientResult);

		var result = await _svc.GetByIdAsync(7);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
		_mapperMock.Verify(m => m.Map<CategoryViewModel>(It.IsAny<CategoryResponse>()), Times.Never);
	}

	[TestMethod]
	public async Task CreateAsync_MapsRequest_MapsResult()
	{
		var vmReq = new CreateCategoryViewModel { Name = "New", Image = null };
		var req = new CreateCategoryRequest { Name = "New", Image = null };
		var resp = new CategoryResponse { Id = 9, Name = "New", ImageUrl = "u9" };
		var clientResult = new Result<CategoryResponse>
		{
			Data = resp,
			Errors = new List<string> { "W" }
		};
		var vm = new CategoryViewModel { Id = 9, Name = "New", ImageUrl = "u9" };

		_mapperMock.Setup(m => m.Map<CreateCategoryRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.CreateAsync(req)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<CategoryViewModel>(resp)).Returns(vm);

		var result = await _svc.CreateAsync(vmReq);

		Assert.AreEqual(vm, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task CreateAsync_ClientReturnsErrors_DataNull()
	{
		var vmReq = new CreateCategoryViewModel { Name = "X", Image = null };
		var req = new CreateCategoryRequest { Name = "X", Image = null };
		var clientResult = new Result<CategoryResponse>
		{
			Data = null,
			Errors = new List<string> { "Err" }
		};

		_mapperMock.Setup(m => m.Map<CreateCategoryRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.CreateAsync(req)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<CategoryViewModel>(null)).Returns((CategoryViewModel?)null!);

		var result = await _svc.CreateAsync(vmReq);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task UpdateAsync_ForwardsClientResult_Success()
	{
		var vmReq = new UpdateCategoryViewModel { Name = "U", Image = null, KeepImage = true };
		var req = new UpdateCategoryRequest { Name = "U", Image = null, KeepImage = true };
		var clientResult = new Result();

		_mapperMock.Setup(m => m.Map<UpdateCategoryRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.UpdateAsync(3, req)).ReturnsAsync(clientResult);

		var result = await _svc.UpdateAsync(3, vmReq);

		Assert.AreSame(clientResult, result);
	}

	[TestMethod]
	public async Task UpdateAsync_ForwardsClientResult_Errors()
	{
		var vmReq = new UpdateCategoryViewModel { Name = "", Image = null, KeepImage = false };
		var req = new UpdateCategoryRequest { Name = "", Image = null, KeepImage = false };
		var clientResult = new Result { Errors = new List<string> { "Bad" } };

		_mapperMock.Setup(m => m.Map<UpdateCategoryRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.UpdateAsync(5, req)).ReturnsAsync(clientResult);

		var result = await _svc.UpdateAsync(5, vmReq);

		Assert.IsTrue(result.HasErrors);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}
}
