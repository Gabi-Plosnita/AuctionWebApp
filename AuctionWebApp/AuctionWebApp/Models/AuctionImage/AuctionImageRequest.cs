namespace AuctionWebApp.Models;

public record AuctionImageRequest
{
	public Stream? Image { get; init; }

	public string? ImageContentType { get; init; }
}
