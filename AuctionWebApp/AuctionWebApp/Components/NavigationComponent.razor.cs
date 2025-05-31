using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class NavigationComponent : ComponentBase
{
	[Inject]
	private NavigationManager NavigationManager { get; set; } = default!;

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
