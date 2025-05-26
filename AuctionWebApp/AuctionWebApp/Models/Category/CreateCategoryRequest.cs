namespace AuctionWebApp.Models;

public record CreateCategoryRequest
{
	public string Name { get; init; } = string.Empty;

	public Stream? Image { get; set; }

	public string? ImageContentType { get; init; }
}
