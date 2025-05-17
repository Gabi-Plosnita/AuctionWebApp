using AuctionWebApp.Enums;

namespace AuctionWebApp.ViewModels;

public class DetailedAuctionViewModel
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public required string Description { get; set; }
	public List<AuctionImageViewModel> Images { get; set; } = new List<AuctionImageViewModel>();
	public decimal StartingPrice { get; set; }
	public decimal CurrentPrice { get; set; }
	public decimal MinBidIncrement { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public AuctionStatus Status { get; set; }
	public required UserViewModel Seller { get; set; }
	public required CategoryViewModel Category { get; set; }
	public List<BidViewModel> Bids { get; set; } = new List<BidViewModel>();
}
