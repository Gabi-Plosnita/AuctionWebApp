using AuctionWebApp.Helpers;
using AuctionWebApp.HttpClients;
using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Services;

public class AuctionService(IAuctionHttpClient _auctionClient, IMapper _mapper) : IAuctionService
{
	public async Task<Result<PagedResultViewModel<PreviewAuctionViewModel>>> GetByFilterAsync(AuctionFilterViewModel filterViewModel)
	{
		var filter = _mapper.Map<AuctionFilterRequest>(filterViewModel);
		var clientResult = await _auctionClient.GetByFilterAsync(filter);
		var previewAuctionViewModels = _mapper.Map<PagedResultViewModel<PreviewAuctionViewModel>>(clientResult.Data);
		var serviceResult = new Result<PagedResultViewModel<PreviewAuctionViewModel>>
		{
			Data = previewAuctionViewModels,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result<PreviewAuctionViewModel>> GetPreviewByIdAsync(int id)
	{
		var clientResult = await _auctionClient.GetPreviewByIdAsync(id);
		var previewAuctionViewModel = _mapper.Map<PreviewAuctionViewModel>(clientResult.Data);
		var serviceResult = new Result<PreviewAuctionViewModel>
		{
			Data = previewAuctionViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result<DetailedAuctionViewModel>> GetDetailedByIdAsync(int id)
	{
		var clientResult = await _auctionClient.GetDetailedByIdAsync(id);
		var detailedAuctionViewModel = _mapper.Map<DetailedAuctionViewModel>(clientResult.Data);
		var serviceResult = new Result<DetailedAuctionViewModel>
		{
			Data = detailedAuctionViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result<BidViewModel>> CreateBidAsync(CreateBidViewModel createBidViewModel)
	{
		var createBidRequest = _mapper.Map<CreateBidRequest>(createBidViewModel);
		var clientResult = await _auctionClient.CreateBidAsync(createBidRequest);
		var bidViewModel = _mapper.Map<BidViewModel>(clientResult.Data);
		var serviceResult = new Result<BidViewModel>
		{
			Data = bidViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}
	public async Task<Result<PreviewAuctionViewModel>> CreateAsync(CreateAuctionViewModel createAuctionViewModel)
	{
		var createAuctionRequest = _mapper.Map<CreateAuctionRequest>(createAuctionViewModel);
		var clientResult = await _auctionClient.CreateAsync(createAuctionRequest);
		var previewAuctionViewModel = _mapper.Map<PreviewAuctionViewModel>(clientResult.Data);
		var serviceResult = new Result<PreviewAuctionViewModel>
		{
			Data = previewAuctionViewModel,
			Errors = clientResult.Errors
		};
		return serviceResult;
	}

	public async Task<Result> UpdateAsync(int id, UpdateAuctionViewModel updateAuctionViewModel)
	{
		var updateAuctionRequest = _mapper.Map<UpdateAuctionRequest>(updateAuctionViewModel);
		var clientResult = await _auctionClient.UpdateAsync(id, updateAuctionRequest);
		return clientResult;
	}

	public async Task<Result> CompleteAuctionAsync(int id)
	{
		var clientResult = await _auctionClient.CompleteAuctionAsync(id);
		return clientResult;
	}
}
