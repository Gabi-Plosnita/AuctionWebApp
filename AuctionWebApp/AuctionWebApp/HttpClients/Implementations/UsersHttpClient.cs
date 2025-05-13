using AuctionWebApp.Helpers;
using AuctionWebApp.Models;

namespace AuctionWebApp.HttpClients;

public class UsersHttpClient : BaseHttpClient, IUsersHttpClient
{
	public UsersHttpClient(HttpClient http) : base(http) { }

	public async Task<Result<UserResponse>> GetByIdAsync(int id)
		=> await SendRequestAsync<UserResponse>($"api/users/{id}", HttpMethod.Get);

	public async Task<Result> UpdateAsync(int id, UpdateUserRequest updateUserRequest)
		=> await SendRequestAsync($"api/users/{id}", HttpMethod.Put, updateUserRequest);

	public async Task<Result<string>> CreateConnectedAccountAsync(CreateConnectedStripeAccountRequest createConnectedAccountRequest)
		=> await SendRequestAsync<string>("api/users/create-connected-account", HttpMethod.Post, createConnectedAccountRequest);

	public async Task<Result<string>> GetAccountLinkAsync(CreateAccountLinkRequest createLinkRequest)
		=> await SendRequestAsync<string>("api/users/generate-account-link", HttpMethod.Post, createLinkRequest);

	public async Task<Result<string>> CreateCustomerAccountAsync(CreateStripeCustomerAccountRequest createStripeCustomerAccountRequest)
		=> await SendRequestAsync<string>("api/users/create-customer-account", HttpMethod.Post, createStripeCustomerAccountRequest);
}
