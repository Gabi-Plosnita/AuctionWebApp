namespace AuctionWebApp.Models;

public record UpdateCategoryRequest
{
	public string Name { get; init; } = string.Empty;
}
