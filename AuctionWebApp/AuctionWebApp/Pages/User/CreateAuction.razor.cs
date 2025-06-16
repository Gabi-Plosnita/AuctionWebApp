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

	private DateTime? _selectedEndDate;

	private TimeSpan? _selectedEndTime;

	private List<CategoryViewModel> _categories = new();

	private bool isLoading = true;

	private bool showErrorComponent = false;


	protected override async Task OnInitializedAsync()
	{
		await InitializeCategoriesAsync();
		if (showErrorComponent || !isLoading)
			return;

		_selectedEndDate = _model.EndTime;
		_selectedEndTime = new TimeSpan(23, 59, 0);
		isLoading = false;
	}

	private async Task InitializeCategoriesAsync()
	{
		var categoriesResult = await CategoryService.GetAllAsync();
		if (categoriesResult.HasErrors || categoriesResult.Data == null)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}
		_categories = categoriesResult.Data.ToList();
	}

	private async Task HandleValidSubmitAsync()
	{
		if (_imageSlots.Count == 0)
		{
			Snackbar.ShowError("Please upload at least one image.");
			return;
		}

		if (!_selectedEndDate.HasValue || !_selectedEndTime.HasValue)
		{
			Snackbar.ShowError("Please select both date and time for the end.");
			return;
		}

		var d = _selectedEndDate.Value;
		var t = _selectedEndTime.Value;
		_model.EndTime = new DateTime(
			d.Year, d.Month, d.Day,
			t.Hours, t.Minutes, t.Seconds
		);

		_model.Images = _imageSlots
			.OrderBy(kvp => kvp.Key)
			.Select(kvp => kvp.Value!)
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
		_selectedEndDate = _model.EndTime;
		_selectedEndTime = new TimeSpan(23, 59, 0);
		_imageSlots.Clear();
		_imagePreviews.Clear();
	}

	private async Task OpenFileExplorer(int index)
	{
		var fileInputId = $"fileInput-{index}";
		await JSRuntime.InvokeVoidAsync("triggerFileClick", fileInputId);
	}
	private async Task OnFileChanged(InputFileChangeEventArgs e, int index)
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
			ResetImageAt(index); 
			return;
		}

		var readImageResult = await FileValidator.GetBase64ImagePreviewAsync(
			file,
			maxSizeInMB: 2
		);
		if (readImageResult.HasErrors)
		{
			Snackbar.ShowErrors(readImageResult.Errors);
			ResetImageAt(index); 
			return;
		}

		_imageSlots[index] = file;
		_imagePreviews[index] = readImageResult.Data;

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