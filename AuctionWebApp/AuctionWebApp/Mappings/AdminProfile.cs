using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class AdminProfile : Profile
{
	public AdminProfile()
	{
		CreateMap<AdminResponse, AdminViewModel>();
		CreateMap<RegisterAdminViewModel, RegisterAdminRequest>();
	}
}
