using AuctionWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class AuctionFilterViewModel
{
	[Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a positive integer")]
	public int? CategoryId { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "SellerId must be a positive integer")]
	public int? SellerId { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "BidderId must be a positive integer")]
	public int? BidderId { get; set; }
	public DriverFilterMode DriverFilterMode { get; init; } = DriverFilterMode.Ignore;

	[Range(1, int.MaxValue, ErrorMessage = "DriverId must be a positive integer")]
	public int? DriverId { get; init; }

	[StringLength(200, ErrorMessage = "TextSearch cannot exceed 200 characters")]
	public string? TextSearch { get; set; }

	[EnumDataType(typeof(AuctionStatus), ErrorMessage = "Invalid status")]
	public AuctionStatus? Status { get; set; }

	[Range(1, 10, ErrorMessage = "Page must be at least 1")]
	public int Page { get; set; } = 1;

	[Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100")]
	public int PageSize { get; set; } = 10;
}
