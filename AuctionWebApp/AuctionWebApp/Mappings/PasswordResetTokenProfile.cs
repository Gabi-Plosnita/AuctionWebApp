using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class PasswordResetTokenProfile : Profile
{
	public PasswordResetTokenProfile()
	{
		CreateMap<PasswordResetViewModel, PasswordResetRequest>();
		CreateMap<ResetPasswordViewModel, ResetPasswordRequest>();
	}
}
