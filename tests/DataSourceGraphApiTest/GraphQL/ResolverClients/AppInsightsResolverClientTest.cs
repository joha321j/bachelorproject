using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DatasourceGraphApi.GraphQL.ResolverClients;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using Moq.Contrib.HttpClient;
using Xunit;

namespace DataSourceGraphApiTest.GraphQL.ResolverClients;

public class AppInsightsResolverClientTest
{
    private const string ClientName = "AppInsights";
    private const string BasePath = "/v1/apps/";
    private static readonly Uri Uri = new($"https://www.example.com{BasePath}");
    private const string AppId = "TestAppId";
    private const string MetricId = "TestMetricId";

    private HttpRequestMessage _requestCaptor = new();
    private string _clientNameCaptor = string.Empty;
    private readonly AppInsightsResolverClient _resolverClient;

    public AppInsightsResolverClientTest()
    {
        var clientHandler = new Mock<HttpMessageHandler>();
        clientHandler
            .SetupAnyRequest()
            .ReturnsResponse(
                JsonSerializer.Serialize(default(object)),
                "application/json")
            .Callback<HttpRequestMessage, CancellationToken>(
                (r, _) => _requestCaptor = r);
        
        var client = clientHandler.CreateClient();
        client.BaseAddress = Uri;
        
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
            .Returns(client)
            .Callback<string>(s => _clientNameCaptor = s);

        _resolverClient = new AppInsightsResolverClient(mockFactory.Object);
    }

    [Fact]  
    public async Task Resolve_Should_UseCorrectClient_When_GettingQueries()
    {
        const string query = "TestQuery";
        const string path = $"{AppId}/query?query={query}";

        await _resolverClient.Resolve(AppId, query);

        _clientNameCaptor.Should().BeEquivalentTo(ClientName);
        _requestCaptor.RequestUri.Should().NotBeNull();
        _requestCaptor.RequestUri!.PathAndQuery.Should().BeEquivalentTo(BasePath + path);
    }


    [Theory]
    [MemberData(nameof(PathTestData))]
    public async Task Resolve_Should_ConstructCorrectPath_When_GettingMetrics(
        List<KeyValuePair<string, string>> parameters,
        string path)
    {
        await _resolverClient.Resolve(AppId, MetricId, parameters);

        _clientNameCaptor.Should().BeEquivalentTo(ClientName);
        _requestCaptor.RequestUri.Should().NotBeNull();
        _requestCaptor.RequestUri!.PathAndQuery.Should().BeEquivalentTo(BasePath + path);
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

    [Theory]
    [MemberData(nameof(EventPathTestData))]
    public async Task Resolve_Should_ConstructCorrectPath_WhenGettingEventsResult(
        string eventType,
        string eventId, 
        string path,
        TimeSpan span)
    {
        await _resolverClient.Resolve(AppId, eventType, eventId, span);

        _clientNameCaptor.Should().BeEquivalentTo(ClientName);
        _requestCaptor.RequestUri.Should().NotBeNull();
        _requestCaptor.RequestUri!.PathAndQuery.Should().BeEquivalentTo(BasePath + path);
    }

    private static IEnumerable<object[]> EventPathTestData()
    {
        const string eventType = "AnEventType";
        const string eventId = "AnEventId";
        yield return new object[]
        {
            eventType,
            eventId,
            $"{AppId}/events/{eventType}/{eventId}",
            null!
        };

        const string secondEventType = "ADifferentEventType";
        const string secondEventId = "BestEventId";
        var timeSpan = TimeSpan.MaxValue;

        yield return new object[]
        {
            secondEventType,
            secondEventId,
            $"{AppId}/events/{secondEventType}/{secondEventId}?timespan=" +
            $"P{timeSpan.Days}DT" +
            $"{timeSpan.Hours}H" +
            $"{timeSpan.Minutes}M" +
            $"{timeSpan.Seconds}." +
            $"{timeSpan.Milliseconds}" +
            $"{timeSpan.Microseconds()}" +
            $"{timeSpan.Nanoseconds() / 100}S",
            timeSpan
        };
    }
}