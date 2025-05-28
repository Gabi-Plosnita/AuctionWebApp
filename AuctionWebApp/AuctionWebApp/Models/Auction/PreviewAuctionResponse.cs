using AuctionWebApp.Enums;

namespace AuctionWebApp.Models;

public record PreviewAuctionResponse
{
	public int Id { get; init; }

	public string Title { get; init; } = string.Empty;

	public List<AuctionImageResponse> Images { get; init; } = new();
	
	public decimal CurrentPrice { get; init; }

	public decimal MinBidIncrement { get; init; }

	public DateTime EndTime { get; init; }

	public AuctionStatus Status { get; init; }

	public int SellerId { get; init; }

	public int CategoryId { get; init; }
}
