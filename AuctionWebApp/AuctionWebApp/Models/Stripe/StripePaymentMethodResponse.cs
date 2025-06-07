namespace AuctionWebApp.Models;

public record StripePaymentMethodResponse
{
	public string? Brand { get; init; }
	public string? Last4 { get; init; }
	public string? ExpMonth { get; init; }
	public string? ExpYear { get; init; }
}
