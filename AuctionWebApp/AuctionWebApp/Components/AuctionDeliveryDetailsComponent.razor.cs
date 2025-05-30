using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class AuctionDeliveryDetailsComponent : ComponentBase
{
	[Parameter]
	public required DetailedAuctionViewModel Auction { get; set; }
}
