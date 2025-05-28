using AuctionWebApp.Atributes;
using AuctionWebApp.Helpers;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class CreateAuctionViewModel
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

	[MinLength(1, ErrorMessage = "You must upload at least one image.")]
	[MaxBrowserFileCount(5, ErrorMessage = "You can upload at most 5 images.")]
	[MaxBrowserFileSize(2 * 1024 * 1024, ErrorMessage = "Each image must be 2 MB or smaller.")]
	[AllowedBrowserExtensions(new[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Only .jpg/.jpeg/.png files are allowed.")]
	public List<IBrowserFile> Images { get; set; } = new();

	[Range(1, double.MaxValue, ErrorMessage = "StartingPrice must be at least 1")]
	[DataType(DataType.Currency)]
	public decimal StartingPrice { get; set; }

	[Range(1, double.MaxValue, ErrorMessage = "MinBidIncrement must be at least 1")]
	[DataType(DataType.Currency)]
	public decimal MinBidIncrement { get; set; }

	[FutureDateAttribute(ErrorMessage = "EndTime must be in the future")]
	public DateTime EndTime { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a positive integer")]
	public int CategoryId { get; set; }
}
