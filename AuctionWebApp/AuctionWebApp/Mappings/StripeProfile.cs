using AuctionWebApp.Models;
using AuctionWebApp.ViewModels;
using AutoMapper;

namespace AuctionWebApp.Mappings;

public class StripeProfile : Profile
{
	public StripeProfile()
	{
		CreateMap<CreateAccountLinkViewModel, CreateAccountLinkRequest>();
		CreateMap<CreateConnectedStripeAccountViewModel, CreateConnectedStripeAccountRequest>();
		CreateMap<CreateStripeCustomerAccountViewModel, CreateStripeCustomerAccountRequest>();
		CreateMap<StripeConnectedAccountResponse, StripeConnectedAccountViewModel>();
	}
}
