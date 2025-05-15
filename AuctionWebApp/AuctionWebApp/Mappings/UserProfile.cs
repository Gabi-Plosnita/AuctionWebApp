using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<RegisterUserRequestViewModel, RegisterUserRequest>();
		CreateMap<UpdateUserRequestViewModel, UpdateUserRequest>();
		CreateMap<UserResponse, UserResponseViewModel>();
	}
}
