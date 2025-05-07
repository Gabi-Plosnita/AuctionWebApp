using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.Models;

public class CreateBidRequest
{
	[Required(ErrorMessage = "AuctionId is required")]
	[Range(1, int.MaxValue, ErrorMessage = "AuctionId must be a positive integer")]
	public int AuctionId { get; set; }

	[Required(ErrorMessage = "Amount is required")]
	[Range(typeof(decimal), "1", "1000000", ErrorMessage = "Amount must be between 1 and 1,000,000")]
	public decimal Amount { get; set; }
}
