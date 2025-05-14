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
	public required string Email { get; set; }

	[Required(ErrorMessage = "Country is required")]
	[RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Country must be a valid ISO 3166-1 alpha-2 code")]
	public required string Country { get; set; }

	[Required(ErrorMessage = "Currency is required")]
	[RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency must be a valid ISO 4217 code")]
	public required string Currency { get; set; }
}
