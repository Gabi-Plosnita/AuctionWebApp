using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.Models;

public class RegisterAdminRequest
{
	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid email address format")]
	public string Email { get; set; } = string.Empty;

	[Required(ErrorMessage = "Password is required")]
	[StringLength(100, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 100 characters")]
	public string Password { get; set; } = string.Empty;
}
