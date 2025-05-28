namespace AuctionWebApp.Models;

public class AuctionImageResponse
{
	public int Id { get; init; }

	public int AuctionId { get; init; }

	public string ImageUrl { get; init; } = string.Empty;
}
