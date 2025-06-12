using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class RequestPasswordReset(IAuthService AuthService, ISnackbar Snackbar) : ComponentBase
{
	private PasswordResetViewModel resetModel = new();

	private string ReturnUrl = "user/login";

	private bool isSubmitting = false;

	private string? SuccessMessage;

	private async Task SubmitRequestAsync()
	{
		isSubmitting = true;

		var result = await AuthService.RequestPasswordResetAsync(resetModel);

		if (!result.HasErrors)
		{
			SuccessMessage = "If an account with that email exists, a password reset link has been sent.";
			resetModel = new();
		}
		else
		{
			Snackbar.ShowErrors(result.Errors);
		}

		isSubmitting = false;
	}

	private string? ValidateEmail(string? email)
	{
		if (string.IsNullOrWhiteSpace(email))
			return "Email is required";

		try
		{
			var addr = new System.Net.Mail.MailAddress(email);
			return null;
		}
		catch
		{
			return "Invalid email address format";
		}
	}
}

