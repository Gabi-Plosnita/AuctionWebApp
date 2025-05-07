namespace AuctionWebApp.Models;

public record CreateBidRequest
{
	public int AuctionId { get; init; }

	public decimal Amount { get; init; }
}
