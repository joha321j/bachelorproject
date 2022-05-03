using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DatasourceGraphApi.GraphQL.ResolverClients;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace DataSourceGraphApiTest.GraphQL.ResolverClients;

public class AppInsightsResolverClientTest
{
    private static readonly Uri Uri = new("http://locohost:8080");
    private const string AppId = "TestAppId";
    private const string Query = "TestQuery";
    private const string MetricId = "TestMetricId";
    private const string BasePath = $"/v1/apps/{AppId}";

    private readonly TestClientHandler _testClientHandler = new();
    private readonly ArgumentCaptor<string> _clientNameCaptor = new();
    private readonly AppInsightsResolverClient _resolverClient;

    public AppInsightsResolverClientTest()
    {
        var client = new HttpClient(_testClientHandler)
        {
            BaseAddress = Uri
        };
        var factory = A.Fake<IHttpClientFactory>();
        A.CallTo(() => factory.CreateClient(_clientNameCaptor))
            .Returns(client);

        _resolverClient = new AppInsightsResolverClient(factory);
    }

    [Fact]
    public async Task Resolve_Should_UseCorrectClient_When_GettingQueries()
    {
        const string path = BasePath + $"/query?query={Query}";

        await _resolverClient.Resolve(AppId, Query);

        _clientNameCaptor.Value.Should().BeEquivalentTo("AppInsights");
        _testClientHandler.Request.Should().NotBeNull();
        _testClientHandler.Request!.PathAndQuery.Should().BeEquivalentTo(path);
    }


    [Theory]
    [MemberData(nameof(PathTestData))]
    public async Task Resolve_Should_ConstructCorrectPath_When_GettingMetrics(
        List<KeyValuePair<string, string>> parameters,
        string path)
    {
        await _resolverClient.Resolve(AppId, MetricId, parameters);

        _clientNameCaptor.Value.Should().BeEquivalentTo("AppInsights");
        _testClientHandler.Request.Should().NotBeNull();
        _testClientHandler.Request!.PathAndQuery.Should().BeEquivalentTo(path);
    }

    private static IEnumerable<object[]> PathTestData()
    {
        const string basePath = BasePath + $"/metrics/{MetricId}";
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