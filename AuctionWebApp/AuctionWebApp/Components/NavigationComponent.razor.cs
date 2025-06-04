using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class NavigationComponent(NavigationManager NavigationManager) : ComponentBase
{
	[Parameter]
	public required string Url { get; set; }

	private void Navigate()
	{
		if (!string.IsNullOrWhiteSpace(Url))
		{
			NavigationManager.NavigateTo(Url);
		}
	}
}
