using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class LoginViewModel
{
	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid email address format")]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; } = string.Empty;

	[Required(ErrorMessage = "Password is required")]
	[RegularExpression(@".*\S+.*", ErrorMessage = "Password is required")]
	[DataType(DataType.Password)]
	public string Password { get; set; } = string.Empty;
}
