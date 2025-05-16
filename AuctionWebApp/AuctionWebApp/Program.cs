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

builder.Services.AddAutoMapper(typeof(Program));

await builder.Build().RunAsync();
