using AuctionWebApp.Enums;

namespace AuctionWebApp.ViewModels;

public class PreviewAuctionResponseViewModel
{
	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;

	public List<AuctionImageRequestViewModel> Images { get; set; } = new List<AuctionImageRequestViewModel>();
	
	public decimal CurrentPrice { get; set; }

	public decimal MinBidIncrement { get; set; }

	public DateTime EndTime { get; set; }

	public AuctionStatus Status { get; set; }

	public int SellerId { get; set; }

	public int CategoryId { get; set; }
}
