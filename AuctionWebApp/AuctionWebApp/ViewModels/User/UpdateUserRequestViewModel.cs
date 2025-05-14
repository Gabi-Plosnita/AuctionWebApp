using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class UpdateUserRequestViewModel
{
	[Required(ErrorMessage = "First name is required")]
	[StringLength(50, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 50 characters")]
	public required string FirstName { get; set; }

	[Required(ErrorMessage = "Last name is required")]
	[StringLength(50, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 50 characters")]
	public required string LastName { get; set; }

	[Required(ErrorMessage = "Address is required")]
	[StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters")]
	public required string Address { get; set; }
}
