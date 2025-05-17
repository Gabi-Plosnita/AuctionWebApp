namespace AuctionWebApp.Models;

public record CreateAuctionRequest
{
	public string Title { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public List<AuctionImageRequest> Images { get; set; } = new();

	public decimal StartingPrice { get; set; }

	public decimal MinBidIncrement { get; set; }

	public DateTime EndTime { get; set; }

	public int CategoryId { get; set; }
}
