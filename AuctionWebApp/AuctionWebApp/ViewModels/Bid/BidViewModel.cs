namespace AuctionWebApp.ViewModels;

public class BidViewModel
{
	public int AuctionId { get; set; }
	public UserViewModel Bidder { get; set; } = default!;
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }

	public BidViewModel() { }

	public BidViewModel(BidViewModel source)
	{
		AuctionId = source.AuctionId;
		Bidder = new UserViewModel(source.Bidder); 
		Amount = source.Amount;
		Date = source.Date;
	}
}
