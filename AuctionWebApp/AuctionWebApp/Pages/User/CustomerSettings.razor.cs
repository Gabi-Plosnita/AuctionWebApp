using AuctionWebApp.HttpClients;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class CustomerSettings(IUserService UserService) : ComponentBase
{
	private StripePaymentMethodViewModel? paymentMethod;
	private bool isCreateMode => paymentMethod == null;
	private string paymentMethodTabName => isCreateMode ? "Add Payment Method" : "Update Payment Method";

	private int activeTabIndex = 0;

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

	private async Task HandleTabChange(int index)
	{
		if (index == 0) 
		{
			await InitializePaymentMethodAsync();
		}
	}
}
