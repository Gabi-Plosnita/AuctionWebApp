using AuctionWebApp;
using AuctionWebApp.HttpClients;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CookieHandler>();

builder.Services
.AddHttpClient<IAuthHttpClient, AuthHttpClient>(client =>
{
	client.BaseAddress = new Uri("https://localhost:5001/");
})
.AddHttpMessageHandler<CookieHandler>();


await builder.Build().RunAsync();
