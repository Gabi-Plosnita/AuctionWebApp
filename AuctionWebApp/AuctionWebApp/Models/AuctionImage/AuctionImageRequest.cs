namespace AuctionWebApp.Models;

public record AuctionImageRequest
{
	public byte[] ImageData { get; init; } = Array.Empty<byte>();

	public string ImageContentType { get; init; } = string.Empty;
}
