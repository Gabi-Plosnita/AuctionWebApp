using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class StripeProfile : Profile
{
	public StripeProfile()
	{
		CreateMap<CreateAccountLinkRequestViewModel, CreateAccountLinkRequest>();
		CreateMap<CreateConnectedStripeAccountRequestViewModel, CreateConnectedStripeAccountRequest>();
		CreateMap<CreateStripeCustomerAccountRequestViewModel, CreateStripeCustomerAccountRequest>();
	}
}
