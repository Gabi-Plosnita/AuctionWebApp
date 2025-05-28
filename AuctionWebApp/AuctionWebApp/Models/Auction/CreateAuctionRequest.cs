using Microsoft.AspNetCore.Components.Forms;

namespace AuctionWebApp.Models;

public record CreateAuctionRequest
{
	public string Title { get; init; } = string.Empty;

	public string Description { get; init; } = string.Empty;

	public List<IBrowserFile> Images { get; init; } = new();

	public decimal StartingPrice { get; init; }

	public decimal MinBidIncrement { get; init; }

	public DateTime EndTime { get; init; }

	public int CategoryId { get; init; }
}
