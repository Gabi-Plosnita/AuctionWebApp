using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.Models;

public class UpdateCategoryRequest
{
	[Required(ErrorMessage = "Name is required")]
	[StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
	public string Name { get; init; } = string.Empty;
}
