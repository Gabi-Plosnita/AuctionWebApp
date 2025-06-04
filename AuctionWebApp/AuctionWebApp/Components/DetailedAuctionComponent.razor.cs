using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AuctionWebApp.Components;

public partial class DetailedAuctionComponent(IJSRuntime JS) : ComponentBase
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
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await JS.InvokeVoidAsync("startCountdown", Auction.EndTime.ToUniversalTime().ToString("o"));
		}
	}
}
