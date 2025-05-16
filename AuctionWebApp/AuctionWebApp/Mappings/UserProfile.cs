using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<RegisterUserViewModel, RegisterUserRequest>();
		CreateMap<UpdateUserViewModel, UpdateUserRequest>();
		CreateMap<UserResponse, UserViewModel>();
	}
}
