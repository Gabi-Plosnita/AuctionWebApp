using AuctionWebApp.Enums;
using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class SellerSettings(IUserService UserService,
									ISnackbar Snackbar) : ComponentBase
{
	private StripeConnectedAccountViewModel? stripeConnectedAccountDetails;

	private CreateConnectedStripeAccountViewModel createConnectedStripeAccountViewModel = new() { AccountType = StripeAccountType.Express };

	private MudForm? _form;

	bool isLoading = true;

	protected override async Task OnInitializedAsync()
	{
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

		var result = await UserService.CreateConnectedAccountAsync(createConnectedStripeAccountViewModel);

		if (result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
			return;
		}

		Snackbar.ShowSuccess("Stripe account created successfully.");
		StateHasChanged();
	}
}
