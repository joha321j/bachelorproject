using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DatasourceApp;
using DatasourceApp.Services;
using Serilog;
using Serilog.Core;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var loggingLevelSwitch = new LoggingLevelSwitch();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(loggingLevelSwitch)
    .WriteTo.BrowserConsole()
    .CreateLogger();

Log.Information("Hello, browser!");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
    .AddScoped<IHttpService, HttpService>()
    .AddScoped<ILocalStorageService, LocalStorageService>();

builder.Services.AddScoped(serviceProvider =>
{
    var apiUrl = new Uri(builder.Configuration["ApiUrl"]
                         ?? throw new ArgumentNullException());

    if (builder.Configuration["UseFakeBackend"] != "true")
        return new HttpClient { BaseAddress = apiUrl };
    
    var fakeBackendHandler = new FakeBackendHandler(serviceProvider.GetService<ILocalStorageService>()
                                                    ?? throw new ArgumentNullException());
    
    return new HttpClient(fakeBackendHandler) { BaseAddress = apiUrl };

});

await builder.Build().RunAsync();
