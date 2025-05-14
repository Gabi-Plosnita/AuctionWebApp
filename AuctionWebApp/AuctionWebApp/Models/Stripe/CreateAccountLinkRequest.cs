using AuctionWebApp.Enums;

namespace AuctionWebApp.Models;

public record CreateAccountLinkRequest
{
	public string ConnectedAccountId { get; init; } = string.Empty;

	public string RefreshUrl { get; init; } = string.Empty;

	public string ReturnUrl { get; init; } = string.Empty;

	public StripeLinkType LinkType { get; init; } = StripeLinkType.AccountOnboarding;
}
