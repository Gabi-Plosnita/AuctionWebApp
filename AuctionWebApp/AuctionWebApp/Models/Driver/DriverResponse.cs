namespace AuctionWebApp.Models;

public record DriverResponse
{
	public int Id { get; set; }

	public Role Role { get; set; }

	public string Email { get; set; } = string.Empty;
}
