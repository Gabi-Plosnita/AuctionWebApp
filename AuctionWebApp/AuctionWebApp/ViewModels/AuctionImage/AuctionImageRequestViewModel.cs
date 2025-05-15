using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class AuctionImageRequestViewModel
{
	[Required(ErrorMessage = "Image data is required")]
	[MinLength(1, ErrorMessage = "Image data cannot be empty")]
	public byte[] ImageData { get; set; } = Array.Empty<byte>();

	[Required(ErrorMessage = "Image content type is required")]
	[RegularExpression(@"^[A-Za-z0-9\-\+\.]+\/[A-Za-z0-9\-\+\.]+$", ErrorMessage = "Invalid image content type format")]
	public string ImageContentType { get; set; } = string.Empty;
}
