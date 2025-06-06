using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace AuctionWebApp.Pages;

public partial class CreateAuction(IJSRuntime JSRuntime) : ComponentBase
{
	private readonly CreateAuctionViewModel _model = new() 
	{ 
		EndTime = DateTime.Now.AddDays(7), 
		StartingPrice = 1,
		MinBidIncrement = 1,
	};
	private readonly Dictionary<int, string> _imagePreviews = new();
	private int _currentImageIndex;

	private async Task HandleValidSubmit()
	{
		var filesToUpload = _model.Images.Values.ToList();
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

		var buffer = new byte[file.Size];
		await file.OpenReadStream(file.Size).ReadAsync(buffer);
		var imageDataUrl = $"data:{file.ContentType};base64,{Convert.ToBase64String(buffer)}";

		_model.Images[_currentImageIndex] = file;
		_imagePreviews[_currentImageIndex] = imageDataUrl;

		StateHasChanged();
	}

	private void DeleteImage(int index)
	{
		_model.Images.Remove(index);
		_imagePreviews.Remove(index);
	}
}