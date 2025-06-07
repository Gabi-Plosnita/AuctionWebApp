using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace AuctionWebApp.Components;

public partial class PaymentMethodComponent(IUserService UserService, 
											IJSRuntime JSRuntime,
											ISnackbar Snackbar) : ComponentBase
{
	[Parameter]
	public bool IsCreateMode { get; set; }

	private string cardholderName = string.Empty;

	private bool isLoading = false;

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await JSRuntime.InvokeVoidAsync("stripePayment.initialize", AppSettings.StripePublishableKey);
		}
	}

	private async Task Submit()
	{
		isLoading = true;

		var result = await JSRuntime.InvokeAsync<StripePaymentMethodResult>("stripePayment.createPaymentMethod", cardholderName);

		if (!string.IsNullOrEmpty(result.PaymentMethodId))
		{
			if(IsCreateMode)
				await CreatePaymentMethodAsync(result.PaymentMethodId);
			else
				await UpdatePaymentMethodAsync(result.PaymentMethodId);
		}
		else
		{
			Snackbar.ShowError("Failed to create payment method");
		}

		ResetFields();
	}

	private async Task CreatePaymentMethodAsync(string paymentMethodId)
	{
		var paymentMethod = new CreateStripeCustomerAccountViewModel
		{
			PaymentMethodId = paymentMethodId,
		};
		var createCustomerAccountResult = await UserService.CreateCustomerAccountAsync(paymentMethod);
		if (createCustomerAccountResult.HasErrors)
		{
			Snackbar.ShowError("Failed to create payment method");
			return;
		}
		if (createCustomerAccountResult.Data == null)
		{
			Snackbar.ShowError("Failed to create payment method");
			return;
		}

		Snackbar.ShowSuccess("Payment method created successfully");
		IsCreateMode = false;
	}

	private async Task UpdatePaymentMethodAsync(string paymentMethodId)
	{
		var updatePaymentMethod = new UpdateStripePaymentMethodViewModel
		{
			PaymentMethodId = paymentMethodId,
		};
		var updatePaymentMethodResult = await UserService.UpdatePaymentMethodAsync(updatePaymentMethod);
		if (updatePaymentMethodResult.HasErrors)
		{
			Snackbar.ShowError("Failed to update payment method");
			return;
		}

		Snackbar.ShowSuccess("Payment method updated successfully");
	}

	private void ResetFields()
	{
		cardholderName = string.Empty;
		isLoading = false;
	}
}

