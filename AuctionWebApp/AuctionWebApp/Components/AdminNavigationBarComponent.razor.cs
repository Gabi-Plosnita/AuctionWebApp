using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class AdminNavigationBarComponent : ComponentBase
{
	[Parameter]
	public bool IsSuperAdmin { get; set; } 
}
