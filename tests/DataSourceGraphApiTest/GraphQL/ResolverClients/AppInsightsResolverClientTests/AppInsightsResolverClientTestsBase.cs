using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using DatasourceGraphApi.GraphQL.ResolverClients;
using Moq;
using Moq.Contrib.HttpClient;

namespace DataSourceGraphApiTest.GraphQL.ResolverClients.AppInsightsResolverClientTests;

public class AppInsightsResolverClientTestsBase
{
    private protected const string ClientName = "AppInsights";
    private protected const string BasePath = "/v1/apps/";
    private static readonly Uri Uri = new($"https://www.example.com{BasePath}");
    private protected const string AppId = "TestAppId";
    private protected const string MetricId = "TestMetricId";

    private protected HttpRequestMessage RequestCaptor = new();
    private protected string ClientNameCaptor = string.Empty;
    private protected readonly AppInsightsResolverClient ResolverClient;

    protected AppInsightsResolverClientTestsBase()
    {
        var clientHandler = new Mock<HttpMessageHandler>();
        clientHandler
            .SetupAnyRequest()
            .ReturnsResponse(
                JsonSerializer.Serialize(default(object)),
                "application/json")
            .Callback<HttpRequestMessage, CancellationToken>(
                (r, _) => RequestCaptor = r);
        
        var client = clientHandler.CreateClient();
        client.BaseAddress = Uri;
        
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
            .Returns(client)
            .Callback<string>(s => ClientNameCaptor = s);

        ResolverClient = new AppInsightsResolverClient(mockFactory.Object);
    }
}