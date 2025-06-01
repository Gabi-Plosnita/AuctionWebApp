using AuctionWebApp.Enums;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class UserDashboard : ComponentBase
{
	[Inject]
	private IAuthService AuthService { get; set; } = default!;

	private string detailedAuctionUrl = "/auctions/{id}/bid";

	private AuctionFilterViewModel myBidsAuctionFilter = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InProgress,
	};

	private int authenticatedUserId;

	protected override async Task OnInitializedAsync()
	{
		var result = await AuthService.GetAuthenticatedUserAsync();
		if(result.HasErrors)
		{
			return;
		}
		if (result.Data == null)
		{
			return;
		}
		authenticatedUserId = result.Data.Id;
		myBidsAuctionFilter.BidderId = authenticatedUserId;
	}

}
