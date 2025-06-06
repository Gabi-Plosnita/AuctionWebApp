using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.Pages;

public partial class CreateAuction(IJSRuntime JSRuntime) : ComponentBase
{
	private readonly CreateAuctionViewModel _model = new() { EndTime = DateTime.Now.AddDays(7) };
	private readonly Dictionary<int, string> _imagePreviews = new();
	private int _currentImageIndex;

	private async Task HandleValidSubmit()
	{
		// Now you can get the files from the dictionary's values
		var filesToUpload = _model.Images.Values.ToList();

		// This is where you would call your API to save the auction
		// For example: await Http.PostAsJsonAsync("api/auctions", _model);
		Console.WriteLine($"Form submitted successfully with {filesToUpload.Count} images!");
	}

	private async Task OpenFileExplorer(int index)
	{
		_currentImageIndex = index;
		await JSRuntime.InvokeVoidAsync("triggerFileClick", "fileInput");
	}

	private async Task OnFileChanged(InputFileChangeEventArgs e)
	{
		var file = e.GetMultipleFiles().FirstOrDefault();
		if (file == null) return;

		// You can add more robust validation here if needed
		var buffer = new byte[file.Size];
		await file.OpenReadStream(file.Size).ReadAsync(buffer);
		var imageDataUrl = $"data:{file.ContentType};base64,{Convert.ToBase64String(buffer)}";

		// Add or replace the file and its preview at the current index
		_model.Images[_currentImageIndex] = file;
		_imagePreviews[_currentImageIndex] = imageDataUrl;

		StateHasChanged();
	}

	private void DeleteImage(int index)
	{
		_model.Images.Remove(index);
		_imagePreviews.Remove(index);
	}

	public class CreateAuctionViewModel
	{
		[Required(ErrorMessage = "Title is required")]
		[StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters")]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = "Description is required")]
		[StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
		public string Description { get; set; } = string.Empty;

		// Changed to a Dictionary to avoid index exceptions.
		[EnsureOneImage(ErrorMessage = "You must upload at least one image.")]
		public Dictionary<int, IBrowserFile> Images { get; set; } = new();

		[Range(1, double.MaxValue, ErrorMessage = "StartingPrice must be at least 1")]
		public decimal StartingPrice { get; set; } = 1.00m;

		[Range(1, double.MaxValue, ErrorMessage = "MinBidIncrement must be at least 1")]
		public decimal MinBidIncrement { get; set; } = 1.00m;

		public DateTime EndTime { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a positive integer")]
		public int CategoryId { get; set; }
	}

	// Custom validation attribute to check if the dictionary has at least one image
	public class EnsureOneImageAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var images = value as Dictionary<int, IBrowserFile>;
			if (images == null || images.Count == 0)
			{
				return new ValidationResult(ErrorMessage);
			}
			return ValidationResult.Success;
		}
	}
}
