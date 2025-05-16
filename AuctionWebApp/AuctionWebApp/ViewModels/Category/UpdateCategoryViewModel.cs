using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class UpdateCategoryViewModel
{
	[Required(ErrorMessage = "Name is required")]
	[StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters")]
	[RegularExpression(@".*\S+.*", ErrorMessage = "Name cannot be blank or whitespace only")]
	public string Name { get; set; } = string.Empty;
}
