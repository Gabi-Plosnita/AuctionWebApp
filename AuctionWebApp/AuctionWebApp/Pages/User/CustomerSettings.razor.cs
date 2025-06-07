using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class CustomerSettings(IUserService UserService) : ComponentBase
{
	private StripePaymentMethodViewModel? paymentMethod;
	private bool isLoading = true;

	protected override async Task OnInitializedAsync()
	{
		await InitializePaymentMethodAsync();
	}

	private async Task InitializePaymentMethodAsync()
	{
		isLoading = true;
		var result = await UserService.GetStripePaymentMethodAsync();
		if (result.HasErrors || result.Data == null)
		{
			isLoading = false;
			return;
		}

		paymentMethod = result.Data;
		isLoading = false;
	}
}
