using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class DriverProfile : Profile
{
	public DriverProfile()
	{
		CreateMap<DriverResponse, DriverResponseViewModel>();
		CreateMap<RegisterDriverRequestViewModel, RegisterDriverRequest>();
	}
}