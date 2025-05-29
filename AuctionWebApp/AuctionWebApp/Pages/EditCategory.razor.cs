using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class EditCategory : ComponentBase
{
	[Inject]
	private ICategoryService CategoryService { get; set; } = default!;

	[Inject]
	private FileHandlerService FileValidator { get; set; } = default!;

	[Inject]
	private NavigationManager NavigationManager { get; set; } = default!;

	[Inject]
	private ISnackbar Snackbar { get; set; } = default!;

	[Parameter] public int Id { get; set; }

	private MudForm? _form;

	private CategoryViewModel _category = new();

	private UpdateCategoryViewModel _model = new();

	private string? _imagePreviewUrl;

	protected override async Task OnInitializedAsync()
	{
		var result = await CategoryService.GetByIdAsync(Id);
		if (result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
			return;
		}
		if (result.Data is null)
		{
			Snackbar.ShowError("Category not found.");
			return;
		}
		_category = result.Data;
		_category.ImageUrl = string.IsNullOrEmpty(_category.ImageUrl) ? null : $"{AppSettings.ApiUrl}{_category.ImageUrl}";
		_model = new UpdateCategoryViewModel() { Name = _category.Name, KeepImage = true};
		_imagePreviewUrl = _category.ImageUrl;
	}

	protected async Task HandleImageChange(InputFileChangeEventArgs e)
	{
		var result = FileValidator.ValidateFile(e.File, allowedExtensions: new[] { ".jpg", ".jpeg", ".png" }, maxSizeInMB: 2);
		if (result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
			ResetImage();
			return;
		}
		var readImageResult = await FileValidator.GetBase64ImagePreviewAsync(e.File, maxSizeInMB: 2);
		if (readImageResult.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
			ResetImage();
			return;
		}
		else
		{
			_imagePreviewUrl = readImageResult.Data;
			_model.Image = e.File;
		}
	}

	protected async Task SubmitAsync()
	{
		if (_form is null)
			return;

		await _form.Validate();

		if (_form.IsValid)
		{
			var result = await CategoryService.UpdateAsync(Id, _model);
			if (result.HasErrors)
			{
				Snackbar.ShowErrors(result.Errors);
			}
			else
			{
				Snackbar.ShowSuccess("Category created successfully.");
			}

			_model = new();
			_imagePreviewUrl = null;
			StateHasChanged();
		}
	}

	private void ResetImage()
	{
		_model.Image = null;
		_imagePreviewUrl = null;
	}
}
