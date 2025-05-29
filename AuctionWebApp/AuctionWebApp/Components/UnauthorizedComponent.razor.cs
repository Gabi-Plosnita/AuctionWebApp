using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class UnauthorizedComponent : ComponentBase
{
	[Inject]
	protected NavigationManager NavigationManager { get; set; } = default!;

	protected void GoHome()
	{
		NavigationManager.NavigateTo("/");
	}
}
