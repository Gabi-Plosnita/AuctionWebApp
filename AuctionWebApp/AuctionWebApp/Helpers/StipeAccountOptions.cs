namespace AuctionWebApp.Helpers;

public static class StripeAccountOptions
{
	public static readonly List<string> Countries = new()
		{
			"US", "CA", "GB", "FR", "DE", "IT", "AU", "NL", "SE", "JP", "RO"
		};

	public static readonly List<string> Currencies = new()
		{
			"USD", "CAD", "GBP", "EUR", "AUD", "JPY", "RON"
		};
}
