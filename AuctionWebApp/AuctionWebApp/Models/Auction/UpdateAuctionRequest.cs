using Microsoft.AspNetCore.Components.Forms;

namespace AuctionWebApp.Models;

public record UpdateAuctionRequest
{
	public string Title { get; init; } = string.Empty;

	public string Description { get; init; } = string.Empty;

	public List<int> ExistingImageIds { get; init; } = new();

	public List<IBrowserFile> NewImages { get; init; } = new();
}
