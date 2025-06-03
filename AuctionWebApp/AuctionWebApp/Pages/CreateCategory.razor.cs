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
									NavigationManager NavigationManager,
									ISnackbar Snackbar) : ComponentBase
{
	protected MudForm? _form;

	protected CreateCategoryViewModel _model = new();

	protected string? _imagePreviewUrl;

	private bool isSuperAdmin;

	private bool isLoading = true;

	protected override async Task OnInitializedAsync()
	{
		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		var user = authState.User;

		isSuperAdmin = user.IsInRole("SuperAdmin");
		isLoading = false;
	}

	protected async Task HandleImageChange(InputFileChangeEventArgs e)
	{
		var result = FileValidator.ValidateFile(e.File, allowedExtensions: new[] { ".jpg", ".jpeg", ".png" }, maxSizeInMB: 2);
		if(result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
			ResetImage();
			return;
		}
		var readImageResult = await FileValidator.GetBase64ImagePreviewAsync(e.File, maxSizeInMB: 2);
		if(readImageResult.HasErrors)
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