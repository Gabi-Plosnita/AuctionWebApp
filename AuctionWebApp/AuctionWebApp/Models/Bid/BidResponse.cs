namespace AuctionWebApp.Models;

public record BidResponse
{
	public string BidderName { get; init; } = string.Empty;
	public decimal Amount { get; init; }
	public DateTime Date { get; init; }
}
