namespace AuctionWebApp.Models;

public record CreateConnectedStripeAccountRequest
{
	public StripeAccountType AccountType { get; init; }

	public string Email { get; init; } = string.Empty;

	public string Country { get; init; } = string.Empty;

	public string Currency { get; init; } = string.Empty;
}
