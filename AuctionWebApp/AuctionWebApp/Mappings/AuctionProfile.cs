using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class AuctionProfile : Profile
{
	public AuctionProfile()
	{
		CreateMap<AuctionFilterViewModel, AuctionFilterRequest>();
		CreateMap<CreateAuctionViewModel, CreateAuctionRequest>();
		CreateMap<DetailedAuctionResponse, DetailedAuctionViewModel>();
		CreateMap<PreviewAuctionResponse, PreviewAuctionViewModel>();
		CreateMap<UpdateAuctionViewModel, UpdateAuctionRequest>();
	}
}
