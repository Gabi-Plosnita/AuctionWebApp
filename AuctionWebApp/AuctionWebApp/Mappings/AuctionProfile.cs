using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class AuctionProfile : Profile
{
	public AuctionProfile()
	{
		CreateMap<AuctionFilterViewModel, AuctionFilterRequest>();
		CreateMap<CreateAuctionViewModel, CreateAuctionRequest>()
			.ForMember(
				dest => dest.Images,
				opt => opt.MapFrom(src =>
					src.Images
					   .Where(kv => kv.Value != null)
					   .Select(kv => kv.Value!)
					   .ToList()
				)
			);
		CreateMap<DetailedAuctionResponse, DetailedAuctionViewModel>();
		CreateMap<PreviewAuctionResponse, PreviewAuctionViewModel>();
		CreateMap<UpdateAuctionViewModel, UpdateAuctionRequest>();
	}
}
