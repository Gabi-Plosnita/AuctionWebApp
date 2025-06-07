namespace AuctionWebApp.Models;

public record UpdateStripePaymentMethodRequest
{
	public required string PaymentMethodId { get; init; }
}
