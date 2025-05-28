using AuctionWebApp.Enums;

namespace AuctionWebApp.Models;

public record AuctionFilterRequest
{
	public int? CategoryId { get; init; }

	public int? SellerId { get; init; }

	public int? BidderId { get; init; }

	public bool IgnoreDriverId { get; init; }

	public int? DriverId { get; init; }

	public string? TextSearch { get; init; }

	public AuctionStatus? Status { get; init; } = AuctionStatus.InProgress;

	public int Page { get; init; } = 1;

	public int PageSize { get; init; } = 10;
}
