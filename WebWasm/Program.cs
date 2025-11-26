using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebWasm;
using WebWasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient to use WebAPI backend
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5126")});

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVoteService, VoteService>();
builder.Services.AddScoped<IPollService, PollService>();
builder.Services.AddScoped<IVoteOptionService, VoteOptionService>();
builder.Services.AddScoped<ILoginService, LoginService>();

await builder.Build().RunAsync();
