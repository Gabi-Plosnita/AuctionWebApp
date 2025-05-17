namespace AuctionWebApp.Models;

public record CreateCategoryRequest
{
	public string Name { get; init; } = string.Empty;
}
