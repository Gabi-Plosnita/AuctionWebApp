namespace AuctionWebApp.Models;

public record CreateStripeCustomerAccountRequest
{
	public string PaymentMethodId { get; init; } = string.Empty;
}
