using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class NotFoundComponent(NavigationManager NavigationManager) : ComponentBase
{
	protected void GoHome() => NavigationManager.NavigateTo("/");
}
