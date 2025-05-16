using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public interface IAuctionHttpClient
{
	Task<Result<PagedResult<PreviewAuctionResponse>>> GetByFilterAsync(AuctionFilterRequest filter);

	Task<Result<PreviewAuctionResponse>> GetPreviewByIdAsync(int id);

	Task<Result<DetailedAuctionResponse>> GetDetailedByIdAsync(int id);

	Task<Result<BidResponse>> CreateBidAsync(CreateBidRequest createBidRequest);

	Task<Result<PreviewAuctionResponse>> CreateAsync(CreateAuctionRequest createAuctionRequest);

	Task<Result> UpdateAsync(int id, UpdateAuctionRequest updateAuctionRequest);

	Task<Result> CompleteAuctionAsync(int id);
}
