using Kioxk.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<Kioxk.Client.Shared.MyEvents>();
builder.Services.AddSingleton<Kioxk.Client.Services.Sync>();
builder.Services.AddSingleton<Kioxk.Client.Services.AutoDefil>();
await builder.Build().RunAsync();
