using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class CreateAuction(IAuctionService AuctionService, 
								   ICategoryService CategoryService,
								   FileHandlerService FileValidator,
								   ISnackbar Snackbar,
								   IJSRuntime JSRuntime) : ComponentBase
{
	private CreateAuctionViewModel _model = new()
	{
		EndTime = DateTime.Now.AddDays(7),
		StartingPrice = 1,
		MinBidIncrement = 1,
	};

	private readonly Dictionary<int, IBrowserFile> _imageSlots = new();
	private readonly Dictionary<int, string> _imagePreviews = new();
	private int _currentImageIndex;

	private EditContext _editContext;

	protected override void OnInitialized()
	{
		_editContext = new EditContext(_model);
	}

	private async Task HandleValidSubmitAsync()
	{
		if (_imageSlots.Count == 0)
		{
			Snackbar.ShowError("Please upload at least one image.");
			return;
		}

		_model.Images = _imageSlots
			.OrderBy(kvp => kvp.Key)
			.Where(kvp => kvp.Value is not null)
			.Select(kvp => kvp.Value)
			.ToList();

		var result = await AuctionService.CreateAsync(_model);
		if (result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
			return;
		}

		Snackbar.ShowSuccess("Auction created successfully!");
		ResetForm();
		StateHasChanged();
	}

	private void ResetForm()
	{
		_model = new CreateAuctionViewModel
		{
			EndTime = DateTime.Now.AddDays(7),
			StartingPrice = 1,
			MinBidIncrement = 1,
		};

		_imageSlots.Clear();
		_imagePreviews.Clear();
		_editContext = new EditContext(_model);
	}

	private async Task OpenFileExplorer(int index)
	{
		_currentImageIndex = index;
		await JSRuntime.InvokeVoidAsync("triggerFileClick", "fileInput");
	}

	private async Task OnFileChanged(InputFileChangeEventArgs e)
	{
		var file = e.GetMultipleFiles().FirstOrDefault();
		if (file == null)
			return;

		var result = FileValidator.ValidateFile(
			file,
			allowedExtensions: new[] { ".jpg", ".jpeg", ".png" },
			maxSizeInMB: 2
		);
		if (result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
			ResetImageAt(_currentImageIndex);
			return;
		}

		var readImageResult = await FileValidator.GetBase64ImagePreviewAsync(
			file,
			maxSizeInMB: 2
		);
		if (readImageResult.HasErrors)
		{
			Snackbar.ShowErrors(readImageResult.Errors);
			ResetImageAt(_currentImageIndex);
			return;
		}

		_imageSlots[_currentImageIndex] = file;
		_imagePreviews[_currentImageIndex] = readImageResult.Data;

		StateHasChanged();
	}

	private void ResetImageAt(int index)
	{
		if (_imageSlots.ContainsKey(index))
			_imageSlots.Remove(index);

		if (_imagePreviews.ContainsKey(index))
			_imagePreviews.Remove(index);

		StateHasChanged();
	}

	private void DeleteImage(int index)
	{
		_imageSlots.Remove(index);
		_imagePreviews.Remove(index);
		StateHasChanged();
	}
}