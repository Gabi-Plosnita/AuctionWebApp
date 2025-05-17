using AuctionWebApp.Enums;

namespace AuctionWebApp.Models;

public record DriverResponse
{
	public int Id { get; init; }

	public Role Role { get; init; }

	public string Email { get; init; } = string.Empty;
}
