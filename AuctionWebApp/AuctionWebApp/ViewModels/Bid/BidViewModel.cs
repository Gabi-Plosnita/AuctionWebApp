namespace AuctionWebApp.ViewModels;

public class BidViewModel
{
	public string BidderName { get; set; } = string.Empty;
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
}
