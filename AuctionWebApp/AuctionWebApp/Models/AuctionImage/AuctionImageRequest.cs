namespace AuctionWebApp.Models;

public record AuctionImageRequest
{
	public Stream? Image { get; set; }

	public string? ImageContentType { get; set; }
}
