namespace AuctionWebApp.Models;

public record UpdateUserRequest
{
	public string FirstName { get; init; } = string.Empty;

	public string LastName { get; init; } = string.Empty;

	public string Address { get; init; } = string.Empty;
}
