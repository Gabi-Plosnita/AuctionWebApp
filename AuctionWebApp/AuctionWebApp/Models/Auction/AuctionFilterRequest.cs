using AuctionWebApp.Enums;

namespace AuctionWebApp.Models;

public record AuctionFilterRequest
{
	public int? CategoryId { get; set; }

	public int? SellerId { get; set; }

	public int? BidderId { get; set; }

	public string? TextSearch { get; set; }

	public AuctionStatus? Status { get; set; } = AuctionStatus.InProgress;

	public int Page { get; set; } = 1;

	public int PageSize { get; set; } = 10;
}
