﻿using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Helpers;

public static class ToastHelper
{
	public static void ShowError(this ISnackbar snackbar, string message)
	{
		snackbar.Clear();

		snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
		snackbar.Configuration.ShowCloseIcon = true;
		snackbar.Configuration.RequireInteraction = true;
		snackbar.Add(message, Severity.Error);
	}

	public static void ShowErrors(this ISnackbar snackbar, List<string> messages)
	{
		snackbar.Clear();

		snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
		snackbar.Configuration.ShowCloseIcon = true;
		snackbar.Configuration.RequireInteraction = true;

		var errorMessage = string.Join("<br />", messages);
		var markupMessage = new MarkupString(errorMessage);
		snackbar.Add(markupMessage, Severity.Error);
	}

	public static void ShowSuccess(this ISnackbar snackbar, string message)
	{
		snackbar.Clear();

		snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
		snackbar.Add(message, Severity.Success, config =>
		{
			config.ShowCloseIcon = true;
			config.RequireInteraction = false;   
			config.VisibleStateDuration = 3000;     
		});
	}
}
