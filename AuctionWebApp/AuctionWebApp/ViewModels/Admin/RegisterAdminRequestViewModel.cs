using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class RegisterAdminRequestViewModel
{
	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid email address format")]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; } = string.Empty;

	[Required(ErrorMessage = "Password is required")]
	[StringLength(100, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 100 characters")]
	[DataType(DataType.Password)]
	public string Password { get; set; } = string.Empty;
}
