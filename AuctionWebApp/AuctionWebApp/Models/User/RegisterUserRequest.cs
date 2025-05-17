namespace AuctionWebApp.Models;

public record RegisterUserRequest
{
	public string Email { get; init; } = string.Empty;

	public string Password { get; init; } = string.Empty;

	public string FirstName { get; init; } = string.Empty;

	public string LastName { get; init; } = string.Empty;

	public string Address { get; init; } = string.Empty;
}
