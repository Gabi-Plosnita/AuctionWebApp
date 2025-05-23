namespace AuctionWebApp.ViewModels;

public class AuthenticatedUserViewModel
{
	public int Id { get; init; }

	public string Email { get; init; } = string.Empty;

	public string Role { get; init; } = string.Empty;
}
