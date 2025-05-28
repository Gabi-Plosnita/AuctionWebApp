namespace AuctionWebApp.Models;

public record UpdateCategoryRequest
{
	public string Name { get; init; } = string.Empty;

	public Stream? Image { get; init; }

	public string? ImageContentType { get; init; }

	public bool KeepImage { get; init; }
}
