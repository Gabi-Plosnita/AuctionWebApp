using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class CreateCategory(ICategoryService CategoryService,
									FileHandlerService FileValidator,
									AuthenticationStateProvider AuthenticationStateProvider,
									ISnackbar Snackbar) : ComponentBase
{
	private MudForm? _form;
	private CreateCategoryViewModel _model = new();
	private string? _imagePreviewUrl;
	private bool isSuperAdmin;
	private bool isLoading = true;

	protected override async Task OnInitializedAsync()
	{
		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		var user = authState.User;
		isSuperAdmin = user.IsInRole("SuperAdmin");
		isLoading = false;
	}

	private async Task HandleFileUpload(IBrowserFile file)
	{
		var validationResult = FileValidator.ValidateFile(
			file,
			allowedExtensions: new[] { ".jpg", ".jpeg", ".png" },
			maxSizeInMB: 2
		);

		if (validationResult.HasErrors)
		{
			Snackbar.ShowErrors(validationResult.Errors);
			ResetImage();
			return;
		}

		var readPreviewResult = await FileValidator.GetBase64ImagePreviewAsync(
			file,
			maxSizeInMB: 2
		);

		if (readPreviewResult.HasErrors)
		{
			Snackbar.ShowErrors(validationResult.Errors);
			ResetImage();
			return;
		}

		_imagePreviewUrl = readPreviewResult.Data;
		_model.Image = file;
	}

	private async Task SubmitAsync()
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