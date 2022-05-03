using DataFakingLibrary;
using DatasourceGraphApi;
using DatasourceGraphApi.GraphQL;
using DatasourceGraphApi.GraphQL.ResolverClients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AppInsightsResolverClient>();
builder.Services.AddHttpClient("AppInsights", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["AppInsightsUrl"]);
}).ConfigurePrimaryHttpMessageHandler(
    () => builder.Configuration["UseFakeBackend"] == "true" ? new FakeInsightHandler() : new HttpClientHandler());

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

app.UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });

app.Run();