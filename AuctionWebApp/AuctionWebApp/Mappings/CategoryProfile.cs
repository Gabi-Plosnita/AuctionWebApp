using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class CategoryProfile : Profile
{
	public CategoryProfile()
	{
		CreateMap<CategoryResponse, CategoryViewModel>();
		CreateMap<CreateCategoryViewModel, CreateCategoryRequest>();
		CreateMap<UpdateCategoryViewModel, UpdateCategoryRequest>();
	}
}
