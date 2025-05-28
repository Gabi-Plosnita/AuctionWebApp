using AuctionWebApp.Atributes;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class UpdateAuctionViewModel
{
	[Required(ErrorMessage = "Title is required")]
	[StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters")]
	[RegularExpression(@".*\S+.*", ErrorMessage = "Title cannot be blank or whitespace only")]
	public string Title { get; set; } = string.Empty;

	[Required(ErrorMessage = "Description is required")]
	[StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
	[RegularExpression(@".*\S+.*", ErrorMessage = "Description cannot be blank or whitespace only")]
	[DataType(DataType.MultilineText)]
	public string Description { get; set; } = string.Empty;

	public List<int> ExistingImageIds { get; set; } = new();

	[MaxBrowserFileCount(5, ErrorMessage = "You can upload at most 5 images.")]
	[MaxBrowserFileSize(2 * 1024 * 1024, ErrorMessage = "Each image must be 2 MB or smaller.")]
	[AllowedBrowserExtensions(new[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Only .jpg/.jpeg/.png files are allowed.")]
	public List<IBrowserFile> NewImages { get; set; } = new();
}
