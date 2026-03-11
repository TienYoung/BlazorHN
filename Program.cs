using BlazorHN;
using BlazorHN.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddFluentUIComponents();
builder.Services.AddSingleton<AppStateService>();
builder.Services.AddScoped<IHackerNewsService, HackerNewsService>(sp => new(new HttpClient { BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/") }));

await builder.Build().RunAsync();
