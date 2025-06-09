using AuctionWebApp;
using AuctionWebApp.HttpClients;
using AuctionWebApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register cookie handler //
builder.Services.AddTransient<CookieHandler>();

// Register HttpClients //
var apiBase = builder.Configuration["ApiBaseUrl"]!;
var baseAddress = new Uri(apiBase);
AppSettings.ApiUrl = apiBase;
AppSettings.StripePublishableKey = builder.Configuration["Stripe:PublishableKey"] ?? string.Empty;

builder.Services.AddScoped(sp =>
  new HttpClient { BaseAddress = new Uri(apiBase) }
);

builder.Services
.AddHttpClient<IAuthHttpClient, AuthHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();

builder.Services
.AddHttpClient<ICategoryHttpClient, CategoryHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();

builder.Services
.AddHttpClient<IAdminHttpClient, AdminHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();

builder.Services
.AddHttpClient<IDriverHttpClient, DriverHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();

builder.Services
.AddHttpClient<IUserHttpClient, UserHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();

builder.Services
.AddHttpClient<IAuctionHttpClient, AuctionHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();

// Register Mappers //
builder.Services.AddAutoMapper(typeof(Program));

// Register Services //
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<FileHandlerService>();

// Register MudBlazor services //
builder.Services.AddMudServices();

// Register authorization service //
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddAuthorizationCore();

// Register HubConnection //
builder.Services.AddTransient(sp =>
{
	var js = sp.GetRequiredService<IJSRuntime>();
	var hubUrl = new Uri(new Uri(apiBase), "hubs/auction");

	return new HubConnectionBuilder()
		.WithUrl(hubUrl, options =>
			options.AccessTokenProvider = () =>
				js.InvokeAsync<string>("blazorExtensions.GetCookie", "access_token").AsTask()
		)
		.WithAutomaticReconnect()
		.Build();
});

await builder.Build().RunAsync();
