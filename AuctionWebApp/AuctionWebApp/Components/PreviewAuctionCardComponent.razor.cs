using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class PreviewAuctionCardComponent(NavigationManager NavigationManager) : ComponentBase
{
	[Parameter]
	public PreviewAuctionViewModel Auction { get; set; } = new PreviewAuctionViewModel();

	[Parameter]
	public string NavigationUrl { get; set; } = string.Empty;

	private string FirstImageUrl => Auction.Images?.FirstOrDefault()?.ImageUrl ?? "placeholder.jpg";

	private void HandleClick()
	{
		NavigationManager.NavigateTo(NavigationUrl);
	}
}