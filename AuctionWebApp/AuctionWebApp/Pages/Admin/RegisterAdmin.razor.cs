using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class RegisterAdmin(IAuthService AuthService,
								   AuthenticationStateProvider AuthenticationStateProvider,
								   ISnackbar Snackbar) : ComponentBase
{
	private MudForm? _form;

	private RegisterAdminViewModel _model = new();

	private bool isSuperAdmin;

	private bool isLoading = true;

	private bool showErrorComponent = false;

	protected override async Task OnInitializedAsync()
	{
		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		var user = authState.User;

		isSuperAdmin = user.IsInRole("SuperAdmin");
		if(!isSuperAdmin)
			showErrorComponent = true;

		isLoading = false;
	}

	private async Task SubmitAsync()
	{
		if (_form is null)
			return;

		await _form.Validate();

		if (_form.IsValid)
		{
			var result = await AuthService.RegisterAdminAsync(_model);
			if (result.HasErrors)
			{
				Snackbar.ShowErrors(result.Errors);
				return;
			}
			if (result.Data == null)
			{
				Snackbar.ShowError("Error while registering admin.");
				return;
			}
			Snackbar.ShowSuccess("Admin registered successfully.");
		}
	}
}
