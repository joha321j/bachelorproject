using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DatasourceGraphApi.GraphQL.ResolverClients;
using FluentAssertions;
using Moq;
using Xunit;

namespace DataSourceGraphApiTest.GraphQL.ResolverClients;

public class AppInsightsResolverClientTest
{
    private const string BasePath = "/v1/apps/";
    private static readonly Uri Uri = new($"https://www.example.com{BasePath}");
    private const string AppId = "TestAppId";
    private const string Query = "TestQuery";
    private const string MetricId = "TestMetricId";


    private readonly TestClientHandler _testClientHandler = new();
    private string _clientNameCaptor = string.Empty;
    private readonly AppInsightsResolverClient _resolverClient;

    public AppInsightsResolverClientTest()
    {
        var client = new HttpClient(_testClientHandler)
        {
            BaseAddress = Uri
        };
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
            .Returns(client)
            .Callback<string>(s => _clientNameCaptor = s);

        _resolverClient = new AppInsightsResolverClient(mockFactory.Object);
    }

    [Fact]  
    public async Task Resolve_Should_UseCorrectClient_When_GettingQueries()
    {
        const string path = $"{AppId}/query?query={Query}";

        await _resolverClient.Resolve(AppId, Query);

        _clientNameCaptor.Should().BeEquivalentTo("AppInsights");
        _testClientHandler.Request.Should().NotBeNull();
        _testClientHandler.Request!.PathAndQuery.Should().BeEquivalentTo(BasePath + path);
    }


    [Theory]
    [MemberData(nameof(PathTestData))]
    public async Task Resolve_Should_ConstructCorrectPath_When_GettingMetrics(
        List<KeyValuePair<string, string>> parameters,
        string path)
    {
        await _resolverClient.Resolve(AppId, MetricId, parameters);

        _clientNameCaptor.Should().BeEquivalentTo("AppInsights");
        _testClientHandler.Request.Should().NotBeNull();
        _testClientHandler.Request!.PathAndQuery.Should().BeEquivalentTo(BasePath + path);
    }

    private static IEnumerable<object[]> PathTestData()
    {
        const string basePath = $"{AppId}/metrics/{MetricId}";
        var parameters = new List<KeyValuePair<string, string>>
        {
            new("firstParameter", "Value"),
            new("anotherParameter", "anotherValue")
        };
        
        yield return new object[]
        {
            new List<KeyValuePair<string, string>>(),
            basePath
        };
        yield return new object[]
        {
            new List<KeyValuePair<string, string>>
            {
                parameters[0]
            },
            basePath + $"?{parameters[0].Key}={parameters[0].Value}"
        };
        yield return new object[]
        {
            parameters,
            basePath +
            $"?{parameters[0].Key}={parameters[0].Value}" +
            $"&{parameters[1].Key}={parameters[1].Value}"
        };
    }
}