using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class CardInformationComponent : ComponentBase
{
	[Parameter]
	public StripePaymentMethodViewModel? PaymentMethod { get; set; }
}
