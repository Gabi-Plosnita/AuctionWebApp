using AuctionWebApp;
using AuctionWebApp.HttpClients;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CookieHandler>();
var baseAddress = new Uri("https://localhost:7149/");

builder.Services
.AddHttpClient<IAuthHttpClient, AuthHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();

builder.Services
.AddHttpClient<ICategoriesHttpClient, CategoriesHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();

builder.Services
.AddHttpClient<IAdminsHttpClient, AdminsHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();

builder.Services
.AddHttpClient<IDriversHttpClient, DriversHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();

builder.Services
.AddHttpClient<IUsersHttpClient, UsersHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();

builder.Services
.AddHttpClient<IAuctionsHttpClient, AuctionsHttpClient>(client =>
{
	client.BaseAddress = baseAddress;
})
.AddHttpMessageHandler<CookieHandler>();


await builder.Build().RunAsync();
