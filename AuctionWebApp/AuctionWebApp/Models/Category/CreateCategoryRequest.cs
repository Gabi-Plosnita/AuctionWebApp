using Microsoft.AspNetCore.Components.Forms;

namespace AuctionWebApp.Models;

public record CreateCategoryRequest
{
	public string Name { get; init; } = string.Empty;

	public IBrowserFile? Image { get; init; }
}
