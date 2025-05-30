namespace AuctionWebApp.ViewModels;

public class BidViewModel
{
	public UserViewModel Bidder { get; set; } = default!;
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
}
