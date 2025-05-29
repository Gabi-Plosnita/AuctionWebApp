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
		var file = e.File;
		_model.Image = file;

		using var stream = file.OpenReadStream(maxAllowedSize: 2 * 1024 * 1024);
		var buffer = new byte[file.Size];
		await stream.ReadAsync(buffer);
		_imagePreviewUrl = $"data:{file.ContentType};base64,{Convert.ToBase64String(buffer)}";
	}

	protected async Task SubmitAsync()
	{
		if (_form is null)
			return;

		await _form.Validate();

		if (_form.IsValid)
		{
			var result = await CategoryService.CreateAsync(_model);
			if(result.HasErrors)
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
}
