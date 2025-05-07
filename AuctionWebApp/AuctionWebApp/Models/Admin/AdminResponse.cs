namespace AuctionWebApp.Models;

public record AdminResponse
{
	public int Id { get; init; }

	public Role Role { get; init; }

	public string Email { get; init; } = string.Empty;
}
