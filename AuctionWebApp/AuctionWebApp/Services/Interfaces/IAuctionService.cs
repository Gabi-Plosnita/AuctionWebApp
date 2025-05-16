using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface IAuctionService
{
	Task<Result<PagedResultViewModel<PreviewAuctionResponseViewModel>>> GetByFilterAsync(AuctionFilterRequestViewModel filter);

	Task<Result<PreviewAuctionResponseViewModel>> GetPreviewByIdAsync(int id);

	Task<Result<DetailedAuctionResponseViewModel>> GetDetailedByIdAsync(int id);

	Task<Result<BidResponseViewModel>> CreateBidAsync(CreateBidRequestViewModel createBidRequest);

	Task<Result<PreviewAuctionResponseViewModel>> CreateAsync(CreateAuctionRequestViewModel createAuctionRequest);

	Task<Result> UpdateAsync(int id, UpdateAuctionRequestViewModel updateAuctionRequest);

	Task<Result> CompleteAuctionAsync(int id);
}
