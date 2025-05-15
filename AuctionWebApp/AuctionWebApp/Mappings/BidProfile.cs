using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class BidProfile : Profile
{
	public BidProfile()
	{
		CreateMap<BidResponse, BidResponseViewModel>();
		CreateMap<CreateBidRequestViewModel, CreateBidRequest>();
	}
}
