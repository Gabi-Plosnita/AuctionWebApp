namespace AuctionWebApp.Models;

public record CreateStripeCustomerAccountRequest
{
	public string PaymentMethodId { get; set; } = string.Empty;
}
