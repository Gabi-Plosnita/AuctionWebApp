using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class CategoryProfile : Profile
{
	public CategoryProfile()
	{
		CreateMap<CategoryResponse, CategoryResponseViewModel>();
		CreateMap<CreateCategoryRequestViewModel, CreateCategoryRequest>();
		CreateMap<UpdateCategoryRequestViewModel, UpdateCategoryRequest>();
	}
}
