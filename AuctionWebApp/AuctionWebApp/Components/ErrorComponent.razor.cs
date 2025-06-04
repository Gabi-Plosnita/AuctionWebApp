using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class ErrorComponent(NavigationManager NavigationManager) : ComponentBase
{
	protected void GoHome() => NavigationManager.NavigateTo("/");
}
