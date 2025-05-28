using Microsoft.AspNetCore.Components.Forms;

namespace AuctionWebApp.Models;

public record UpdateCategoryRequest
{
	public string Name { get; init; } = string.Empty;

	public IBrowserFile? Image { get; init; }

	public bool KeepImage { get; init; }
}
