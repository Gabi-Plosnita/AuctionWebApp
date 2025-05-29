using Microsoft.AspNetCore.Components;
using MudBlazor;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuctionWebApp.Helpers;

public static class ToastHelper
{
	public static void ShowError(this ISnackbar snackbar, string message)
	{
		snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
		snackbar.Configuration.ShowCloseIcon = true;
		snackbar.Configuration.RequireInteraction = true;
		snackbar.Add(message, Severity.Error);
	}

	public static void ShowErrors(this ISnackbar snackbar, List<string> messages)
	{
		snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
		snackbar.Configuration.ShowCloseIcon = true;
		snackbar.Configuration.RequireInteraction = true;

		var errorMessage = string.Join("<br />", messages);
		var markupMessage = new MarkupString(errorMessage);
		snackbar.Add(markupMessage, Severity.Error);
	}
}
