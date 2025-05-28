using AuctionWebApp.Enums;

namespace AuctionWebApp.Models;

public record DetailedAuctionResponse
{
	public int Id { get; init; }

	public string Title { get; init; } = "";

	public string Description { get; init; } = "";

	public List<AuctionImageResponse> Images { get; init; } = new();

	public decimal StartingPrice { get; init; }

	public decimal CurrentPrice { get; init; }

	public decimal MinBidIncrement { get; init; }

	public DateTime StartTime { get; init; }

	public DateTime EndTime { get; init; }

	public AuctionStatus Status { get; init; }

	public UserResponse Seller { get; init; } = default!;

	public DriverResponse? Driver { get; init; } = null;

	public CategoryResponse Category { get; init; } = default!;

	public List<BidResponse> Bids { get; init; } = new();
}
