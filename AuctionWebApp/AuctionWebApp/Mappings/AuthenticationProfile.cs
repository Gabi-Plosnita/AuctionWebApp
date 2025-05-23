using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class AuthenticationProfile : Profile
{
	public AuthenticationProfile()
	{
		CreateMap<LoginViewModel, LoginRequest>();
		CreateMap<AuthenticatedUserResponse, AuthenticatedUserViewModel>();
	}
}
