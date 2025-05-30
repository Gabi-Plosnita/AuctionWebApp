namespace AuctionWebApp.Models;

public record BidResponse
{
	public UserResponse Bidder { get; init; } = default!; 
	public decimal Amount { get; init; }
	public DateTime Date { get; init; }
}
