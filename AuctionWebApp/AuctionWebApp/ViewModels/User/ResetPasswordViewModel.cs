using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class ResetPasswordViewModel
{
	public string Token { get; set; } = string.Empty;

	[Required(ErrorMessage = "Password is required")]
	[StringLength(100, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 100 characters")]
	public string NewPassword { get; set; } = string.Empty;
}
