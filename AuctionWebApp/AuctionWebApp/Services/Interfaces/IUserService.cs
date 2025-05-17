using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface IUserService
{
	Task<Result<UserViewModel>> GetByIdAsync(int id);

	Task<Result> UpdateAsync(int id, UpdateUserViewModel updateUserViewModel);

	Task<Result<string>> CreateConnectedAccountAsync(CreateConnectedStripeAccountViewModel createConnectedAccountViewModel);

	Task<Result<string>> GetAccountLinkAsync(CreateAccountLinkViewModel createLinkViewModel);

	Task<Result<string>> CreateCustomerAccountAsync(CreateStripeCustomerAccountViewModel createCustomerAccountViewModel);
}
