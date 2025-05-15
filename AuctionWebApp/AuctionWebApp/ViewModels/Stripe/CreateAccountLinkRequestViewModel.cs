using AuctionWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class CreateAccountLinkRequestViewModel
{
	[Required(ErrorMessage = "ConnectedAccountId is required")]
	[RegularExpression(@"^acct_[A-Za-z0-9]+$", ErrorMessage = "Invalid Stripe account ID format")]
	public string ConnectedAccountId { get; set; } = string.Empty;

	[Required(ErrorMessage = "RefreshUrl is required")]
	[Url(ErrorMessage = "Invalid URL format")]
	[RegularExpression(@"^https:\/\/.*", ErrorMessage = "Refresh URL must start with https://")]
	public string RefreshUrl { get; set; } = string.Empty;

	[Required(ErrorMessage = "ReturnUrl is required")]
	[Url(ErrorMessage = "Invalid URL format")]
	[RegularExpression(@"^https:\/\/.*", ErrorMessage = "Return URL must start with https://")]
	public string ReturnUrl { get; set; } = string.Empty;

	[EnumDataType(typeof(StripeLinkType), ErrorMessage = "Invalid link type")]
	public StripeLinkType LinkType { get; set; } = StripeLinkType.AccountOnboarding;
}
