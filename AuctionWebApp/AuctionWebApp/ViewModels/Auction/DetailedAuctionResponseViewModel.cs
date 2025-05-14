using AuctionWebApp.Enums;

namespace AuctionWebApp.ViewModels;

public class DetailedAuctionResponseViewModel
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public required string Description { get; set; }
	public List<AuctionImageRequestViewModel> Images { get; set; } = new List<AuctionImageRequestViewModel>();
	public decimal StartingPrice { get; set; }
	public decimal CurrentPrice { get; set; }
	public decimal MinBidIncrement { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public AuctionStatus Status { get; set; }
	public required UserResponseViewModel Seller { get; set; }
	public required CategoryResponseViewModel Category { get; set; }
	public List<BidResponseViewModel> Bids { get; set; } = new List<BidResponseViewModel>();
}
