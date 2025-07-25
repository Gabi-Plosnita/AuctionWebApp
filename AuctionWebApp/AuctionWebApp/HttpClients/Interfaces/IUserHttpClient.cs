﻿using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public interface IUserHttpClient
{
	Task<Result<UserResponse>> GetByIdAsync(int id);

	Task<Result> UpdateAsync(int id, UpdateUserRequest updateUserRequest);

	Task<Result<string>> CreateConnectedAccountAsync(CreateConnectedStripeAccountRequest createConnectedAccountRequest);

	Task<Result<string>> GetAccountLinkAsync(CreateAccountLinkRequest createLinkRequest);

	Task<Result<string>> CreateCustomerAccountAsync(CreateStripeCustomerAccountRequest createCustomerAccountRequest);

	Task<Result> UpdatePaymentMethodAsync(UpdateStripePaymentMethodRequest updatePaymentMethodRequest);

	Task<Result<StripeConnectedAccountResponse>> GetStripeConnectedAccountDetails();

	Task<Result<StripePaymentMethodResponse>> GetStripePaymentMethodAsync();
}
