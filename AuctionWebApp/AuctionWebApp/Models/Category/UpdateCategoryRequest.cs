namespace AuctionWebApp.Models;

public record UpdateCategoryRequest
{
	public string Name { get; init; } = string.Empty;

	public Stream? Image { get; set; }

	public bool KeepImage { get; set; }
}
