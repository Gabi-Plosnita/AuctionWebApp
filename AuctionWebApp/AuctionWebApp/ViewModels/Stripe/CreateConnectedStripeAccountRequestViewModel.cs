using AuctionWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class CreateConnectedStripeAccountRequestViewModel
{
	[Required(ErrorMessage = "AccountType is required")]
	[EnumDataType(typeof(StripeAccountType), ErrorMessage = "Invalid account type")]
	public StripeAccountType AccountType { get; set; }

	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid email address format")]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; } = string.Empty;

	[Required(ErrorMessage = "Country is required")]
	[RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Country must be a valid ISO 3166-1 alpha-2 code")]
	public string Country { get; set; } = string.Empty;

	[Required(ErrorMessage = "Currency is required")]
	[RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency must be a valid ISO 4217 code")]
	public string Currency { get; set; } = string.Empty;
}
