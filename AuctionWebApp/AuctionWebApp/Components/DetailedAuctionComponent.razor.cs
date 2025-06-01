using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class DetailedAuctionComponent : ComponentBase
{
	[Parameter]
	public required DetailedAuctionViewModel Auction { get; set; }

	private List<string> imageUrls = new List<string>();

	protected override void OnParametersSet()
	{
		if (Auction?.Images != null && Auction.Images.Any())
		{
			imageUrls = Auction.Images.Select(i => i.ImageUrl).ToList();
		}
	}
}
