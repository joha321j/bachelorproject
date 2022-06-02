using DataFakingLibrary;
using DataSourceGraphApi.GraphQL;
using DataSourceGraphApi.GraphQL.ResolverClients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AppInsightsResolverClient>();
builder.Services.AddScoped<YoutubeResolverClient>();

builder.Services.AddHttpClient("AppInsights", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["AppInsightsUrl"]);
}).ConfigurePrimaryHttpMessageHandler(
    () => builder.Configuration["UseFakeBackend"] == "true" ? new FakeInsightHandler() : new HttpClientHandler());

builder.Services.AddHttpClient("Youtube", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["YoutubeUrl"]);
}).ConfigurePrimaryHttpMessageHandler(
    () => builder.Configuration["UseFakeBackend"] == "true" ? new FakeYoutubeHandler() : new HttpClientHandler());

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

app.UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });

app.Run();

namespace DataSourceGraphApi
{
    public class Program
    {
        
    }
}