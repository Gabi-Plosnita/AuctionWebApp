using AuctionWebApp.Enums;

namespace AuctionWebApp.ViewModels;

public class DriverViewModel
{
	public int Id { get; set; }

	public Role Role { get; set; }

	public string Email { get; set; } = string.Empty;

	public DriverViewModel() { }

	public DriverViewModel(DriverViewModel source)
	{
		Id = source.Id;
		Role = source.Role;
		Email = source.Email;
	}
}
