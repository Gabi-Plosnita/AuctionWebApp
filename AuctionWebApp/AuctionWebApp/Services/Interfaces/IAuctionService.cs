using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface IAuctionService
{
	Task<Result<PagedResultViewModel<PreviewAuctionViewModel>>> GetByFilterAsync(AuctionFilterViewModel filterViewModel);

	Task<Result<PreviewAuctionViewModel>> GetPreviewByIdAsync(int id);

	Task<Result<DetailedAuctionViewModel>> GetDetailedByIdAsync(int id);

	Task<Result<BidViewModel>> CreateBidAsync(CreateBidViewModel createBidViewModel);

	Task<Result<PreviewAuctionViewModel>> CreateAsync(CreateAuctionViewModel createAuctionViewModel);

	Task<Result> UpdateAsync(int id, UpdateAuctionViewModel updateAuctionViewModel);

	Task<Result> CompleteAuctionAsync(int id);
}
