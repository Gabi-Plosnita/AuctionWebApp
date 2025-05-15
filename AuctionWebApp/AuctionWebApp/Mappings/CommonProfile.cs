using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class CommonProfile : Profile
{
	public CommonProfile()
	{
		CreateMap(typeof(PagedResult<>), typeof(PagedResultViewModel<>)).ReverseMap();
	}
}
