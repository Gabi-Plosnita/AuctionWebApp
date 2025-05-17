using AuctionWebApp.Enums;

namespace AuctionWebApp.ViewModels;

public class PreviewAuctionViewModel
{
	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;

	public List<AuctionImageViewModel> Images { get; set; } = new List<AuctionImageViewModel>();
	
	public decimal CurrentPrice { get; set; }

	public decimal MinBidIncrement { get; set; }

	public DateTime EndTime { get; set; }

	public AuctionStatus Status { get; set; }

	public int SellerId { get; set; }

	public int CategoryId { get; set; }
}
