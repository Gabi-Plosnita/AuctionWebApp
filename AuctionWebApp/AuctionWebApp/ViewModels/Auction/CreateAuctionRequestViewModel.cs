using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class CreateAuctionRequestViewModel
{
	[Required(ErrorMessage = "Title is required")]
	[StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters")]
	public required string Title { get; set; }

	[Required(ErrorMessage = "Description is required")]
	[StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
	public required string Description { get; set; }

	[MinLength(1, ErrorMessage = "At least one image is required")]
	public List<AuctionImageRequestViewModel> Images { get; set; } = new();

	[Range(1, double.MaxValue, ErrorMessage = "StartingPrice must be at least 1")]
	public decimal StartingPrice { get; set; }

	[Range(1, double.MaxValue, ErrorMessage = "MinBidIncrement must be at least 1")]
	public decimal MinBidIncrement { get; set; }

	[Required(ErrorMessage = "EndTime is required")]
	[FutureDateAttribute(ErrorMessage = "EndTime must be in the future")]
	public DateTime EndTime { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a positive integer")]
	public int CategoryId { get; set; }
}
