using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class CreateCategory : ComponentBase
{
	[Inject]

	private ICategoryService CategoryService { get; set; } = default!;

	[Inject]
	private NavigationManager NavigationManager { get; set; } = default!;

	[Inject]
	private ISnackbar Snackbar { get; set; } = default!;

	[Parameter]
	public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

	protected MudForm? _form;

	protected CreateCategoryViewModel _model = new();

	protected string? _imagePreviewUrl;

	protected async Task HandleImageChange(InputFileChangeEventArgs e)
	{
		try
		{
			var file = e.File;

			var allowedMimeTypes = new[] { "image/jpeg", "image/png" };
			if (!allowedMimeTypes.Contains(file.ContentType.ToLower()))
			{
				Snackbar.ShowError("Only JPG and PNG images are allowed.");
				ResetImage();
				return;
			}

			if (file.Size > 2 * 1024 * 1024)
			{
				Snackbar.ShowError("Image must be 2 MB or smaller.");
				ResetImage();
				return;
			}

			_model.Image = file;

			using var stream = file.OpenReadStream(maxAllowedSize: 2 * 1024 * 1024);
			var buffer = new byte[file.Size];
			await stream.ReadAsync(buffer);
			_imagePreviewUrl = $"data:{file.ContentType};base64,{Convert.ToBase64String(buffer)}";
		}
		catch (IOException)
		{
			Snackbar.Add("Failed to process the image. Please ensure it's a valid file.", Severity.Error);
			ResetImage();
		}
		catch (Exception ex)
		{
			Snackbar.Add($"Unexpected error: {ex.Message}", Severity.Error);
			ResetImage();
		}
	}

	protected async Task SubmitAsync()
	{
		if (_form is null)
			return;

		await _form.Validate();

		if (_form.IsValid)
		{
			var result = await CategoryService.CreateAsync(_model);
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