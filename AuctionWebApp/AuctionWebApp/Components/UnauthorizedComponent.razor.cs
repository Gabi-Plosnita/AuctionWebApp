﻿using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class UnauthorizedComponent(NavigationManager NavigationManager) : ComponentBase
{
	protected void GoHome() => NavigationManager.NavigateTo("/");
}
