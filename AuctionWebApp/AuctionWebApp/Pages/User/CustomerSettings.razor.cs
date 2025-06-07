using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class CustomerSettings(IUserService UserService) : ComponentBase
{
	private StripePaymentMethodViewModel? paymentMethod;
	private bool isCreateMode => paymentMethod == null;
	private string paymentMethodTabName => isCreateMode ? "Add Payment Method" : "Update Payment Method";

	private bool isLoading = true;

	private bool isCardDetailsLoading = false;

	private int _activePanelIndex;
	private int ActivePanelIndex
	{
		get => _activePanelIndex;
		set
		{
			if (_activePanelIndex != value)
			{
				_activePanelIndex = value;

				if (_activePanelIndex == 0)
				{
					_ = InitializePaymentMethodAsync();
				}
			}
		}
	}

	protected override async Task OnInitializedAsync()
	{
		isLoading = true;
		await InitializePaymentMethodAsync(); 
		isLoading = false;
	}

	private async Task InitializePaymentMethodAsync()
	{
		isCardDetailsLoading = true;
		StateHasChanged(); 

		var result = await UserService.GetStripePaymentMethodAsync();
		if (result.HasErrors || result.Data == null)
		{
			paymentMethod = null; 
		}
		else
		{
			paymentMethod = result.Data;
		}

		isCardDetailsLoading = false;
		StateHasChanged(); 
	}
}
