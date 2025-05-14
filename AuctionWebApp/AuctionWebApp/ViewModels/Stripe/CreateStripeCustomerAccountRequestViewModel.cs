using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.ViewModels;

public class CreateStripeCustomerAccountRequestViewModel
{
	[Required(ErrorMessage = "PaymentMethodId is required")]
	[RegularExpression(@"^pm_[A-Za-z0-9]+$", ErrorMessage = "Invalid PaymentMethodId format")]
	public required string PaymentMethodId { get; set; }
}
