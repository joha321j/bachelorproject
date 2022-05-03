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
    
    [Fact]
    public async Task Resolve_Should_UseCorrectClient()
    {
        const string path = BasePath + $"/query?query={Query}";
        var clientNameCaptor = new ArgumentCaptor<string>();

        var handler = new TestClientHandler();
        var client = new HttpClient(handler)
        {
            BaseAddress = Uri
        };
        var factory = A.Fake<IHttpClientFactory>();
        A.CallTo(() => factory.CreateClient(clientNameCaptor))
            .Returns(client);
        
        var resolverClient = new AppInsightsResolverClient(factory);

        await resolverClient.Resolve(AppId, Query);

        clientNameCaptor.Value.Should().BeEquivalentTo("AppInsights");
        handler.Request.Should().NotBeNull();
        handler.Request!.PathAndQuery.Should().BeEquivalentTo(path);
    }

    [Theory]
    [MemberData(nameof(PathTestData))]
    public async Task Resolve_Should_ConstructCorrectPath(List<KeyValuePair<string, string>> parameters, string path)
    {
        var clientNameCaptor = new ArgumentCaptor<string>();

        var handler = new TestClientHandler();
        var client = new HttpClient(handler)
        {
            BaseAddress = Uri
        };
        var factory = A.Fake<IHttpClientFactory>();
        A.CallTo(() => factory.CreateClient(clientNameCaptor))
            .ReturnsLazily(() => client);
        var resolverClient = new AppInsightsResolverClient(factory);

        await resolverClient.Resolve(AppId, MetricId, parameters);

        clientNameCaptor.Value.Should().BeEquivalentTo("AppInsights");
        handler.Request.Should().NotBeNull();
        handler.Request!.PathAndQuery.Should().BeEquivalentTo(path);
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
            basePath + $"?{parameters[0].Key}={parameters[0].Value}&{parameters[1].Key}={parameters[1].Value}"
        };
    }
}