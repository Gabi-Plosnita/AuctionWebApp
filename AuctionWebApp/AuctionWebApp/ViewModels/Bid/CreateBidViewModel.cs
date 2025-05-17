using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class CreateBidViewModel
{
	[Range(1, int.MaxValue, ErrorMessage = "AuctionId must be a positive integer")]
	public int AuctionId { get; set; }

	[Range(typeof(decimal), "1", "1000000", ErrorMessage = "Amount must be between 1 and 1,000,000")]
	[DataType(DataType.Currency)]
	public decimal Amount { get; set; }
}
