using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class AuctionDeliveryDetailsComponent : ComponentBase
{
	[Parameter]
	public required DetailedAuctionViewModel Auction { get; set; }

	private UserViewModel? winner;

	protected override async Task OnParametersSetAsync()
	{
		if (Auction?.Bids != null && Auction.Bids.Any())
		{
			winner = Auction.Bids
				.OrderByDescending(b => b.Amount)
				.FirstOrDefault()?.Bidder;
		}
	}
}
