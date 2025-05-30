using AuctionWebApp.Enums;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class AdminDashboard : ComponentBase
{
	private AuctionFilterViewModel auctionsWithNoDriver = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InTransit,
		IgnoreDriverId = false,
		DriverId = null,
		PageSize = 10,
		Page = 1
	};
}
