using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class RegisterUser(IAuthService AuthService, 
								  ISnackbar Snackbar,
								  NavigationManager NavigationManager) : ComponentBase
{
	private MudForm? _form;

	private RegisterUserViewModel _model = new();

	private async Task SubmitAsync()
	{
		if (_form is null)
			return;

		await _form.Validate();

		if (_form.IsValid)
		{
			var result = await AuthService.RegisterUserAsync(_model);
			if (result.HasErrors)
			{
				Snackbar.ShowErrors(result.Errors);
				return;
			}
			if (result.Data == null)
			{
				Snackbar.ShowError("Error while registering user.");
				return;
			}
			Snackbar.ShowSuccess("User registered successfully. You can now log in.");
		}
	}

	private void OnLoginClicked()
	{
		NavigationManager.NavigateTo("user/login");
	}
}
