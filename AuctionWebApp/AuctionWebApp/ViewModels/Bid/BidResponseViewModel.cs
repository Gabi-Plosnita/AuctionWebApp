namespace AuctionWebApp.ViewModels;

public class BidResponseViewModel
{
	public required string BidderName { get; set; }
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
}
