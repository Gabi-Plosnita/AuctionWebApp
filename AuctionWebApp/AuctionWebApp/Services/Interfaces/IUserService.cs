using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;

namespace AuctionWebApp.Services;

public interface IUserService
{
	Task<Result<UserResponseViewModel>> GetByIdAsync(int id);

	Task<Result> UpdateAsync(int id, UpdateUserRequestViewModel updateUserRequest);

	Task<Result<string>> CreateConnectedAccountAsync(CreateConnectedStripeAccountRequestViewModel createConnectedAccountRequest);

	Task<Result<string>> GetAccountLinkAsync(CreateAccountLinkRequestViewModel createLinkRequest);

	Task<Result<string>> CreateCustomerAccountAsync(CreateStripeCustomerAccountRequestViewModel createStripeCustomerAccountRequest);
}
