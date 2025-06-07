using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public class UserHttpClient : BaseHttpClient, IUserHttpClient
{
	public UserHttpClient(HttpClient http) : base(http) { }

	public async Task<Result<UserResponse>> GetByIdAsync(int id)
		=> await SendRequestAsync<UserResponse>($"api/users/{id}", HttpMethod.Get);

	public async Task<Result> UpdateAsync(int id, UpdateUserRequest updateUserRequest)
		=> await SendRequestAsync($"api/users/{id}", HttpMethod.Put, updateUserRequest);

	public async Task<Result<string>> CreateConnectedAccountAsync(CreateConnectedStripeAccountRequest request)
		=> await SendRequestAsync<string>("api/users/connected-account", HttpMethod.Post, request);

	public async Task<Result<string>> GetAccountLinkAsync(CreateAccountLinkRequest request)
		=> await SendRequestAsync<string>("api/users/connected-account/account-link", HttpMethod.Post, request);

	public async Task<Result<string>> CreateCustomerAccountAsync(CreateStripeCustomerAccountRequest request)
		=> await SendRequestAsync<string>("api/users/customer-account", HttpMethod.Post, request);

	public async Task<Result> UpdatePaymentMethodAsync(UpdateStripePaymentMethodRequest request)
		=> await SendRequestAsync("api/users/customer-account", HttpMethod.Put, request);

	public async Task<Result<StripeConnectedAccountResponse>> GetStripeConnectedAccountDetails()
		=> await SendRequestAsync<StripeConnectedAccountResponse>("api/users/connected-account", HttpMethod.Get);

	public async Task<Result<StripePaymentMethodResponse>> GetStripePaymentMethodAsync()
		=> await SendRequestAsync<StripePaymentMethodResponse>("api/users/customer-account/payment-method", HttpMethod.Get);
}
