namespace AuctionWebApp.ViewModels;

public class StripeConnectedAccountViewModel
{
	public string AccountId { get; set; }
	public string? BusinessName { get; set; }
	public string? Email { get; set; }
	public string Country { get; set; }
	public bool ChargesEnabled { get; set; }
	public bool PayoutsEnabled { get; set; }
	public bool DetailsSubmitted { get; set; }
}
