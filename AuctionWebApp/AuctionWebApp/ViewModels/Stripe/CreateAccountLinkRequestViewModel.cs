using AuctionWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class CreateAccountLinkRequestViewModel
{
	[Required(ErrorMessage = "ConnectedAccountId is required")]
	[RegularExpression(@"^acct_[A-Za-z0-9]+$", ErrorMessage = "Invalid Stripe account ID format")]
	public required string ConnectedAccountId { get; set; }

	[Required(ErrorMessage = "RefreshUrl is required")]
	[Url(ErrorMessage = "Invalid URL format")]
	public required string RefreshUrl { get; set; }

	[Required(ErrorMessage = "ReturnUrl is required")]
	[Url(ErrorMessage = "Invalid URL format")]
	public required string ReturnUrl { get; set; }

	[EnumDataType(typeof(StripeLinkType), ErrorMessage = "Invalid link type")]
	public StripeLinkType LinkType { get; set; } = StripeLinkType.AccountOnboarding;
}
