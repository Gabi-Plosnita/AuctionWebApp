using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class RegisterDriver(IAuthService AuthService,
								    AuthenticationStateProvider AuthenticationStateProvider,
								    ISnackbar Snackbar) : ComponentBase
{
	private MudForm? _form;

	private RegisterDriverViewModel _model = new();

	private bool isSuperAdmin;

	private bool isLoading = true;

	protected override async Task OnInitializedAsync()
	{
		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		var user = authState.User;

		isSuperAdmin = user.IsInRole("SuperAdmin");
		isLoading = false;
	}

	private async Task SubmitAsync()
	{
		if (_form is null)
			return;

		await _form.Validate();

		if (_form.IsValid)
		{
			var result = await AuthService.RegisterDriverAsync(_model);
			if (result.HasErrors)
			{
				Snackbar.ShowErrors(result.Errors);
				return;
			}
			if (result.Data == null)
			{
				Snackbar.ShowError("Error while registering driver.");
				return;
			}
			Snackbar.ShowSuccess("Driver registered successfully.");
		}
	}
}
