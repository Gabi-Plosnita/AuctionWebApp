using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class AuctionImageProfile : Profile
{
	public AuctionImageProfile()
	{
		CreateMap<AuctionImageRequest, AuctionImageRequestViewModel>().ReverseMap();
	}
}
