using AuctionWebApp;
using AuctionWebApp.HttpClients;
using AuctionWebApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register cookie handler //
builder.Services.AddTransient<CookieHandler>();

// Register HttpClients //
var baseAddress = new Uri("https://localhost:7149/");

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

// Register authorization service //
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddScoped<JwtAuthStateProvider>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
