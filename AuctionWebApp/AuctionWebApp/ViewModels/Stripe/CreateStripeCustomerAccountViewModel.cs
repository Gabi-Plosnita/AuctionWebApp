using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class CreateStripeCustomerAccountViewModel
{
	[Required(ErrorMessage = "PaymentMethodId is required")]
	[RegularExpression(@"^pm_[A-Za-z0-9]+$", ErrorMessage = "Invalid PaymentMethodId format")]
	public string PaymentMethodId { get; set; } = string.Empty;
}
