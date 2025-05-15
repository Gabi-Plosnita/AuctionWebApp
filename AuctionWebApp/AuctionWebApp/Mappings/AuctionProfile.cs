using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class AuctionProfile : Profile
{
	public AuctionProfile()
	{
		CreateMap<AuctionFilterRequestViewModel, AuctionFilterRequest>();
		CreateMap<CreateAuctionRequestViewModel, CreateAuctionRequest>();
		CreateMap<DetailedAuctionResponse, DetailedAuctionResponseViewModel>();
		CreateMap<PreviewAuctionResponse, PreviewAuctionResponseViewModel>();
		CreateMap<UpdateAuctionRequestViewModel, UpdateAuctionRequest>();
	}
}
