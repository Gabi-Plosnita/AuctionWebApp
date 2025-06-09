namespace AuctionWebApp.Models;

public record BidResponse
{
	public int AuctionId { get; init; }
	public UserResponse Bidder { get; init; } = default!; 
	public decimal Amount { get; init; }
	public DateTime Date { get; init; }
}
