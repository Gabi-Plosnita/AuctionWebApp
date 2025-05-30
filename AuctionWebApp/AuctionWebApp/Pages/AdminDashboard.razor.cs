using AuctionWebApp.Enums;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Pages;

public partial class AdminDashboard : ComponentBase
{
	private AuctionFilterViewModel auctionsWithNoDriver = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InTransit,
		DriverFilterMode = DriverFilterMode.NoDriver,
	};

	private AuctionFilterViewModel auctionsWithDriver = new AuctionFilterViewModel
	{
		Status = AuctionStatus.InTransit,
		DriverFilterMode = DriverFilterMode.AnyDriver,
	};
}
