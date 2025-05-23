namespace AuctionWebApp.Models;

public record AuthenticatedUserResponse
{
	public int Id { get; init; }

	public string Email { get; init; } = string.Empty;

	public string Role { get; init; } = string.Empty;
}