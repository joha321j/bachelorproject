using DataFakingLibrary;
using DatasourceGraphApi;
using DatasourceGraphApi.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ResolverClient>();
builder.Services.AddHttpClient("AppInsights", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["AppInsightsUrl"]);
}).ConfigurePrimaryHttpMessageHandler(() => new FakeInsightHandler());

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

app.UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });

app.Run();