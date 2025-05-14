using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class UpdateAuctionRequestViewModel
{
	[Required(ErrorMessage = "Title is required")]
	[StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters")]
	public required string Title { get; set; }

	[Required(ErrorMessage = "Description is required")]
	[StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
	public required string Description { get; set; }

	[MinLength(1, ErrorMessage = "At least one image is required")]
	public List<AuctionImageRequestViewModel> Images { get; set; } = new();
}
