using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class UpdateCategoryRequestViewModel
{
	[Required(ErrorMessage = "Name is required")]
	[StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
	public required string Name { get; set; }
}
