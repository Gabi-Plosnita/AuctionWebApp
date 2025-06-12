using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Web;

namespace AuctionWebApp.Pages;

public partial class ResetPassword(IAuthService AuthService, 
						           NavigationManager NavigationManager,
						           ISnackbar Snackbar) : ComponentBase
{
	protected ResetPasswordViewModel resetModel = new();

	protected bool isSubmitting = false;

	protected string? SuccessMessage;

	protected override void OnInitialized()
	{
		var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
		var query = HttpUtility.ParseQueryString(uri.Query);
		var token = query["token"];

		if (!string.IsNullOrWhiteSpace(token))
		{
			resetModel.Token = token;
		}
		else
		{
			SuccessMessage = "Invalid or missing reset token.";
		}
	}

	protected async Task SubmitResetAsync()
	{
		isSubmitting = true;

		var result = await AuthService.ResetPasswordAsync(resetModel);

		if (!result.HasErrors)
		{
			SuccessMessage = "Password successfully reset! You can now log in.";
			resetModel = new(); 
		}
		else
		{
			Snackbar.ShowErrors(result.Errors);
		}

		isSubmitting = false;
	}
}