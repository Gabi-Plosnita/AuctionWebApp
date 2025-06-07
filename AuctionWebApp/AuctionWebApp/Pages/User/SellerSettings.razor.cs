using AuctionWebApp.Enums;
using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class SellerSettings(IUserService UserService,
									IJSRuntime JSRuntime,
									NavigationManager NavigationManager,
									ISnackbar Snackbar) : ComponentBase
{
	private StripeConnectedAccountViewModel? stripeConnectedAccountDetails;

	private CreateConnectedStripeAccountViewModel createConnectedStripeAccountViewModel = new() { AccountType = StripeAccountType.Express };

	private MudForm? _form;

	bool isLoading = true;

	protected override async Task OnInitializedAsync()
	{
		await LoadConnectedAccountDetailsAsync();
	}
	private async Task LoadConnectedAccountDetailsAsync()
	{
		isLoading = true;
		var result = await UserService.GetStripeConnectedAccountDetailsAsync();
		stripeConnectedAccountDetails = result.Data;
		isLoading = false;
	}

	private async Task CreateConnectedAccountAsync()
	{
		if (_form is null)
			return;

		await _form.Validate();

		if (!_form.IsValid)
			return;

		isLoading = true;
		var result = await UserService.CreateConnectedAccountAsync(createConnectedStripeAccountViewModel);

		if (result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
			isLoading = false;
			return;
		}

		Snackbar.ShowSuccess("Stripe account created successfully.");
		await LoadConnectedAccountDetailsAsync();
	}

	private async Task EditAccountAsync()
	{
		if (stripeConnectedAccountDetails == null)
			return;

		var linkViewModel = CreateLink();
		if (linkViewModel == null)
		{
			Snackbar.ShowError("Stripe account details are not available.");
			return;
		}

		isLoading = true;
		var createOnboardingLinkResult = await UserService.GetAccountLinkAsync(linkViewModel);

		if (createOnboardingLinkResult.HasErrors)
		{
			Snackbar.ShowErrors(createOnboardingLinkResult.Errors);
			isLoading = false;
			return;
		}

		if(string.IsNullOrEmpty(createOnboardingLinkResult.Data))
		{
			Snackbar.ShowError("Failed to create edit Stripe account link.");
			isLoading = false;
			return;
		}

		var editUrl = createOnboardingLinkResult.Data;

		try
		{
			await JSRuntime.InvokeVoidAsync("openInNewTab", editUrl);
		}
		catch (JSException ex)
		{
			Snackbar.ShowError($"Failed to open Stripe onboarding link");
			isLoading = false;
			return;
		}
		catch (Exception ex)
		{
			Snackbar.ShowError($"An unexpected error occurred");
			isLoading = false;
			return;
		}
		isLoading = false;
	}

	private CreateAccountLinkViewModel? CreateLink()
	{
		if (stripeConnectedAccountDetails == null || string.IsNullOrEmpty(stripeConnectedAccountDetails.AccountId))
			return null;

		return new CreateAccountLinkViewModel
		{
			ConnectedAccountId = stripeConnectedAccountDetails.AccountId,
			ReturnUrl = NavigationManager.Uri,
			RefreshUrl = NavigationManager.Uri,
			LinkType = StripeLinkType.AccountOnboarding
		};
	}
}
