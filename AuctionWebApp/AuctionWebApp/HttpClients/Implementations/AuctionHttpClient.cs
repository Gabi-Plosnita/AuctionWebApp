using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public class AuctionHttpClient : BaseHttpClient, IAuctionHttpClient
{
	public AuctionHttpClient(HttpClient http) : base(http) { }

	public async Task<Result<PagedResult<PreviewAuctionResponse>>> GetByFilterAsync(AuctionFilterRequest filter)
		=> await SendRequestWithQueryAsync<PagedResult<PreviewAuctionResponse>>("api/auctions", HttpMethod.Get, filter); 

	public async Task<Result<PreviewAuctionResponse>> GetPreviewByIdAsync(int id)
		=> await SendRequestAsync<PreviewAuctionResponse>($"api/auctions/{id}/preview", HttpMethod.Get);

	public async Task<Result<DetailedAuctionResponse>> GetDetailedByIdAsync(int id)
		=> await SendRequestAsync<DetailedAuctionResponse>($"api/auctions/{id}/detailed", HttpMethod.Get);

	public async Task<Result<BidResponse>> CreateBidAsync(CreateBidRequest createBidRequest)
		=> await SendRequestAsync<BidResponse>("api/auctions/createBid", HttpMethod.Post, createBidRequest);

	public async Task<Result<PreviewAuctionResponse>> CreateAsync(CreateAuctionRequest createAuctionRequest)
		=> await SendRequestAsync<PreviewAuctionResponse>("api/auctions", HttpMethod.Post, createAuctionRequest);

	public async Task<Result> UpdateAsync(int id, UpdateAuctionRequest updateAuctionRequest)
		=> await SendRequestAsync($"api/auctions/{id}", HttpMethod.Put, updateAuctionRequest);

	public async Task<Result> AssignDriverAsync(int id, AssignDriverRequest assignDriverRequest)
		=> await SendRequestAsync($"api/auctions/{id}/driver", HttpMethod.Put, assignDriverRequest);

	public async Task<Result> CompleteAuctionAsync(int id)
		=> await SendRequestAsync($"api/auctions/{id}/complete", HttpMethod.Post);
}
