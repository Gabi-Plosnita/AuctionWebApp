using AuctionWebApp.HttpClients;
using AuctionWebApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AuctionWebApp.Pages;

public partial class CategoryTestPage : ComponentBase
{
	[Inject] private ICategoryHttpClient _categoryClient { get; set; } = default!;

	private string Name = string.Empty;
	private IBrowserFile? SelectedFile;
	private bool IsSubmitting;
	private List<string>? Errors;

	private void OnFileChanged(InputFileChangeEventArgs e)
		=> SelectedFile = e.File;

	private async Task Submit()
	{
		Errors = null;
		IsSubmitting = true;


		var dto = new CreateCategoryRequest
		{
			Name = Name,
			Image = SelectedFile
		};

		var result = await _categoryClient.CreateAsync(dto);

		if (result.Errors?.Any() == true)
		{
			Errors = result.Errors;
		}
		else
		{
			Name = string.Empty;
			SelectedFile = null;
		}

		IsSubmitting = false;
	}
}
