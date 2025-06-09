using AuctionWebApp.Enums;

namespace AuctionWebApp.ViewModels;

public class UserViewModel
{
	public int Id { get; set; }

	public Role Role { get; set; }

	public string Email { get; set; } = string.Empty;

	public string FirstName { get; set; } = string.Empty;

	public string LastName { get; set; } = string.Empty;

	public string Address { get; set; } = string.Empty;

	public UserViewModel() { }

	public UserViewModel(UserViewModel source)
	{
		Id = source.Id;
		Role = source.Role;
		Email = source.Email;
		FirstName = source.FirstName;
		LastName = source.LastName;
		Address = source.Address;
	}
}
