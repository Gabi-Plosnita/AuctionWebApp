using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class RegisterUserRequestViewModel
{
	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid email address format")]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; } = string.Empty;

	[Required(ErrorMessage = "Password is required")]
	[StringLength(100, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 100 characters")]
	[RegularExpression(@".*\S+.*", ErrorMessage = "Password cannot be blank or whitespace only")]
	[DataType(DataType.Password)]
	public string Password { get; set; } = string.Empty;

	[Required(ErrorMessage = "First name is required")]
	[StringLength(50, ErrorMessage = "First name must be between 1 and 50 characters")]
	[RegularExpression(@".*\S+.*", ErrorMessage = "First name cannot be blank or whitespace only")]
	public string FirstName { get; set; } = string.Empty;

	[Required(ErrorMessage = "Last name is required")]
	[StringLength(50, ErrorMessage = "Last name must be between 1 and 50 characters")]
	[RegularExpression(@".*\S+.*", ErrorMessage = "Last name cannot be blank or whitespace only")]
	public string LastName { get; set; } = string.Empty;

	[Required(ErrorMessage = "Address is required")]
	[StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters")]
	[RegularExpression(@".*\S+.*", ErrorMessage = "Address cannot be blank or whitespace only")]
	public string Address { get; set; } = string.Empty;
}
