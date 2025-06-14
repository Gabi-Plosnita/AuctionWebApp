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
public class AuctionServiceTests
{
	private Mock<IAuctionHttpClient> _clientMock = default!;
	private Mock<IMapper> _mapperMock = default!;
	private AuctionService _svc = default!;

	[TestInitialize]
	public void Setup()
	{
		_clientMock = new Mock<IAuctionHttpClient>();
		_mapperMock = new Mock<IMapper>();
		_svc = new AuctionService(_clientMock.Object, _mapperMock.Object);
	}

	[TestMethod]
	public async Task GetPreviewByIdAsync_MapsAndPropagates()
	{
		var response = new PreviewAuctionResponse { Id = 42, Title = "Z" };
		var clientResult = new Result<PreviewAuctionResponse> { Data = response, Errors = new List<string> { "nf" } };
		var vm = new PreviewAuctionViewModel { Id = 42, Title = "Z" };

		_clientMock.Setup(c => c.GetPreviewByIdAsync(42)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<PreviewAuctionViewModel>(response)).Returns(vm);

		var result = await _svc.GetPreviewByIdAsync(42);

		Assert.AreEqual(vm, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetDetailedByIdAsync_MapsAndPropagates()
	{
		var response = new DetailedAuctionResponse { Id = 99, Title = "Details", Description = "D" };
		var clientResult = new Result<DetailedAuctionResponse> { Data = response, Errors = new List<string>() };
		var vm = new DetailedAuctionViewModel { Id = 99, Title = "Details", Description = "D" };

		_clientMock.Setup(c => c.GetDetailedByIdAsync(99)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<DetailedAuctionViewModel>(response)).Returns(vm);

		var result = await _svc.GetDetailedByIdAsync(99);

		Assert.AreEqual(vm, result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task CreateBidAsync_MapsRequest_MapsResult()
	{
		var vmReq = new CreateBidViewModel { AuctionId = 5, Amount = 123m };
		var req = new CreateBidRequest { AuctionId = 5, Amount = 123m };
		var resp = new BidResponse { AuctionId = 5, Amount = 123m, Date = DateTime.UtcNow };
		var clientResult = new Result<BidResponse> { Data = resp };
		var vm = new BidViewModel { AuctionId = 5, Amount = 123m, Date = resp.Date };

		_mapperMock.Setup(m => m.Map<CreateBidRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.CreateBidAsync(req)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<BidViewModel>(resp)).Returns(vm);

		var result = await _svc.CreateBidAsync(vmReq);

		Assert.AreEqual(vm, result.Data);
	}

	[TestMethod]
	public async Task CreateAsync_MapsRequest_MapsResult()
	{
		var vmReq = new CreateAuctionViewModel { Title = "New" };
		var req = new CreateAuctionRequest { Title = "New" };
		var resp = new PreviewAuctionResponse { Id = 7, Title = "New" };
		var clientResult = new Result<PreviewAuctionResponse> { Data = resp };
		var vm = new PreviewAuctionViewModel { Id = 7, Title = "New" };

		_mapperMock.Setup(m => m.Map<CreateAuctionRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.CreateAsync(req)).ReturnsAsync(clientResult);
		_mapperMock.Setup(m => m.Map<PreviewAuctionViewModel>(resp)).Returns(vm);

		var result = await _svc.CreateAsync(vmReq);

		Assert.AreEqual(vm, result.Data);
	}

	[TestMethod]
	public async Task UpdateAsync_ForwardsClientResult()
	{
		var clientRes = new Result();
		_mapperMock.Setup(m => m.Map<UpdateAuctionRequest>(It.IsAny<UpdateAuctionViewModel>()))
				   .Returns(new UpdateAuctionRequest());
		_clientMock.Setup(c => c.UpdateAsync(3, It.IsAny<UpdateAuctionRequest>()))
				   .ReturnsAsync(clientRes);

		var actual = await _svc.UpdateAsync(3, new UpdateAuctionViewModel());
		Assert.AreSame(clientRes, actual);
	}

	[TestMethod]
	public async Task AssignDriverAsync_ForwardsClientResult()
	{
		var clientRes = new Result();
		_clientMock.Setup(c => c.AssignDriverAsync(11, It.Is<AssignDriverRequest>(r => r.DriverId == 22)))
				   .ReturnsAsync(clientRes);

		var actual = await _svc.AssignDriverAsync(11, 22);
		Assert.AreSame(clientRes, actual);
	}

	[TestMethod]
	public async Task CompleteAuctionAsync_ForwardsClientResult()
	{
		var clientRes = new Result();
		_clientMock.Setup(c => c.CompleteAuctionAsync(55)).ReturnsAsync(clientRes);

		var actual = await _svc.CompleteAuctionAsync(55);
		Assert.AreSame(clientRes, actual);
	}

	[TestMethod]
	public async Task GetByFilterAsync_ClientReturnsErrors_NoMappingOccurs_DataIsNull()
	{
		var vmFilter = new AuctionFilterViewModel { Page = 1 };
		var reqFilter = new AuctionFilterRequest { Page = 1 };
		var clientResult = new Result<PagedResult<PreviewAuctionResponse>> { Data = null, Errors = new List<string> { "Server error" } };

		_mapperMock.Setup(m => m.Map<AuctionFilterRequest>(vmFilter)).Returns(reqFilter);
		_clientMock.Setup(c => c.GetByFilterAsync(reqFilter)).ReturnsAsync(clientResult);

		var result = await _svc.GetByFilterAsync(vmFilter);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task GetPreviewByIdAsync_ClientReturnsErrors_DataIsNull()
	{
		var clientResult = new Result<PreviewAuctionResponse> { Data = null, Errors = new List<string> { "Not found" } };
		_clientMock.Setup(c => c.GetPreviewByIdAsync(123)).ReturnsAsync(clientResult);

		var result = await _svc.GetPreviewByIdAsync(123);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task CreateBidAsync_ClientReturnsErrors_DataIsNull()
	{
		var vmReq = new CreateBidViewModel { AuctionId = 9, Amount = 50m };
		var req = new CreateBidRequest { AuctionId = 9, Amount = 50m };
		var clientResult = new Result<BidResponse> { Data = null, Errors = new List<string> { "Bid too low" } };

		_mapperMock.Setup(m => m.Map<CreateBidRequest>(vmReq)).Returns(req);
		_clientMock.Setup(c => c.CreateBidAsync(req)).ReturnsAsync(clientResult);

		var result = await _svc.CreateBidAsync(vmReq);

		Assert.IsNull(result.Data);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}

	[TestMethod]
	public async Task UpdateAsync_ClientReturnsErrors_ErrorsPropagated()
	{
		var clientResult = new Result { Errors = new List<string> { "Unauthorized" } };
		_mapperMock.Setup(m => m.Map<UpdateAuctionRequest>(It.IsAny<UpdateAuctionViewModel>())).Returns(new UpdateAuctionRequest());
		_clientMock.Setup(c => c.UpdateAsync(5, It.IsAny<UpdateAuctionRequest>())).ReturnsAsync(clientResult);

		var result = await _svc.UpdateAsync(5, new UpdateAuctionViewModel());

		Assert.IsTrue(result.HasErrors);
		CollectionAssert.AreEqual(clientResult.Errors, result.Errors);
	}
}