using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class LoginRequestViewModel
{
	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid email address format")]
	public required string Email { get; set; }

	[Required(ErrorMessage = "Password is required")]
	[StringLength(100, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 100 characters")]
	public required string Password { get; set; }
}
