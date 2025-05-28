using AuctionWebApp.Atributes;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class CreateCategoryViewModel
{
	[Required(ErrorMessage = "Name is required")]
	[StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters")]
	[RegularExpression(@".*\S+.*", ErrorMessage = "Name cannot be blank or whitespace only")]
	public string Name { get; set; } = string.Empty;

	[MaxBrowserFileSize(2 * 1024 * 1024, ErrorMessage = "Each image must be 2 MB or smaller.")]
	[AllowedBrowserExtensions(new[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Only .jpg/.jpeg/.png files are allowed.")]
	public IBrowserFile? Image { get; set; }
}
