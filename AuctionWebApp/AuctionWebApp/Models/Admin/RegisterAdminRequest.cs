namespace AuctionWebApp.Models;

public record RegisterAdminRequest
{
	public string Email { get; init; } = string.Empty;

	public string Password { get; init; } = string.Empty;
}
