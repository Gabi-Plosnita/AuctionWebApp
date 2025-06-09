using AuctionWebApp.Enums;

namespace AuctionWebApp.ViewModels;

public class DetailedAuctionViewModel
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public List<AuctionImageViewModel> Images { get; set; } = new List<AuctionImageViewModel>();
	public decimal StartingPrice { get; set; }
	public decimal CurrentPrice { get; set; }
	public decimal MinBidIncrement { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public AuctionStatus Status { get; set; }
	public UserViewModel Seller { get; set; }
	public DriverViewModel? Driver { get; set; } = null;
	public CategoryViewModel Category { get; set; }
	public List<BidViewModel> Bids { get; set; } = new List<BidViewModel>();

	public DetailedAuctionViewModel() { }

	public DetailedAuctionViewModel(DetailedAuctionViewModel source)
	{
		Id = source.Id;
		Title = source.Title;
		Description = source.Description;
		StartingPrice = source.StartingPrice;
		CurrentPrice = source.CurrentPrice;
		MinBidIncrement = source.MinBidIncrement;
		StartTime = source.StartTime;
		EndTime = source.EndTime;
		Status = source.Status;

		Images = source.Images.Select(i => new AuctionImageViewModel(i)).ToList();
		Seller = new UserViewModel(source.Seller);
		Driver = source.Driver != null ? new DriverViewModel(source.Driver) : null;
		Category = new CategoryViewModel(source.Category);
		Bids = source.Bids.Select(b => new BidViewModel(b)).ToList();
	}
}
