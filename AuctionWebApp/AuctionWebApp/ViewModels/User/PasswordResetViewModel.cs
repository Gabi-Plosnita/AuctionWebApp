using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class PasswordResetRequest
{
	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid email address format")]
	public string Email { get; set; } = string.Empty;
}
