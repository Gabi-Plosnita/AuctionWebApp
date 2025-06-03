using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class EditCategory(ICategoryService CategoryService,
								  FileHandlerService FileValidator,
								  NavigationManager NavigationManager,
								  AuthenticationStateProvider AuthenticationStateProvider,
								  ISnackbar Snackbar) : ComponentBase
{
	[Parameter] public int Id { get; set; }

	private MudForm? _form;

	private CategoryViewModel _category = new();

	private UpdateCategoryViewModel _model = new();

	private string? _imagePreviewUrl;

	private bool isSuperAdmin;

	private bool isLoading = true;

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

		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		var user = authState.User;

		isSuperAdmin = user.IsInRole("SuperAdmin");
		isLoading = false;
	}

	private async Task HandleImageChange(InputFileChangeEventArgs e)
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

	private async Task SubmitAsync()
	{
		if (_form is null)
			return;

		await _form.Validate();

		if (_form.IsValid)
		{
			if(_model.Image != null)
				_model.KeepImage = false; 

			var result = await CategoryService.UpdateAsync(Id, _model);
			if (result.HasErrors)
			{
				Snackbar.ShowErrors(result.Errors);
				_model = new UpdateCategoryViewModel() { Name = _category.Name, KeepImage = true };
				ResetImage();
				StateHasChanged();
				return;
			}
			else
			{
				Snackbar.ShowSuccess("Category updated successfully.");
				NavigationManager.NavigateTo("/admin/categories-dashboard");
			}
		}
	}

	private void ResetImage()
	{
		_model.Image = null;
		_imagePreviewUrl = _category.ImageUrl;
	}
}
