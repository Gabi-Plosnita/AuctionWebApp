using AuctionWebApp.Enums;

namespace AuctionWebApp.ViewModels;

public class AdminViewModel
{
	public int Id { get; set; }

	public Role Role { get; set; }

	public string Email { get; set; } = string.Empty;
}
