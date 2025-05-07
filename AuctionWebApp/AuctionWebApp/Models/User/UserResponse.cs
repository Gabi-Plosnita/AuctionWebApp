namespace AuctionWebApp.Models;

public record UserResponse
{
	public int Id { get; init; }

	public Role Role { get; init; }

	public string Email { get; init; } = string.Empty;

	public string FirstName { get; init; } = string.Empty;

	public string LastName { get; init; } = string.Empty;

	public string Address { get; init; } = string.Empty;
}
