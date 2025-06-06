using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace AuctionWebApp.Pages;

public partial class CreateAuction(IJSRuntime JSRuntime) : ComponentBase
{
	private readonly CreateAuctionViewModel _model = new() { EndTime = DateTime.Now.AddDays(7) };
	private readonly Dictionary<int, string> _imagePreviews = new();
	private int _currentImageIndex;

	private async Task HandleValidSubmit()
	{
		// This is where you would call your API to save the auction
		// For example: await Http.PostAsJsonAsync("api/auctions", _model);
		Console.WriteLine("Form submitted successfully!");
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

		// Basic validation before reading the file
		var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
		var extension = Path.GetExtension(file.Name).ToLowerInvariant();
		if (!allowedExtensions.Contains(extension))
		{
			// Handle error - maybe a notification service
			return;
		}
		if (file.Size > 2 * 1024 * 1024)
		{
			// Handle error
			return;
		}

		var buffer = new byte[file.Size];
		await file.OpenReadStream(file.Size).ReadAsync(buffer);
		var imageDataUrl = $"data:{file.ContentType};base64,{Convert.ToBase64String(buffer)}";

		// Replace image if one already exists at this index
		var existingFile = _model.Images.ElementAtOrDefault(_currentImageIndex);
		if (existingFile != null)
		{
			_model.Images.RemoveAt(_currentImageIndex);
		}

		_model.Images.Insert(_currentImageIndex, file);
		_imagePreviews[_currentImageIndex] = imageDataUrl;

		StateHasChanged();
	}

	private void DeleteImage(int index)
	{
		if (_imagePreviews.ContainsKey(index))
		{
			_imagePreviews.Remove(index);
			var fileToRemove = _model.Images.ElementAtOrDefault(index);
			if (fileToRemove != null)
			{
				_model.Images.Remove(fileToRemove);
			}
		}
	}
}
