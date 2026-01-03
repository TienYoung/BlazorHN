using BlazorHN;
using BlazorHN.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HackerNewsService(new HttpClient { BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/") }));

await builder.Build().RunAsync();
