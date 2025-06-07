namespace AuctionWebApp.Helpers;

public class StripePaymentMethodResult
{
	public string PaymentMethodId { get; set; } = string.Empty;
	public string Error { get; set; } = string.Empty;
}
