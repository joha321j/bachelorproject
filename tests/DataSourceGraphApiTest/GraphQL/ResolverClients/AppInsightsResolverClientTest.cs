using System;
using System.Net.Http;
using System.Threading.Tasks;
using DatasourceGraphApi;
using DatasourceGraphApi.GraphQL.ResolverClients;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace DataSourceGraphApiTest.GraphQL.ResolverClients;

public class AppInsightsResolverClientTest
{
    [Fact]
    public async Task Resolve_Should_UseCorrectClient()
    {
        const string appId = "TestAppId";
        const string query = "TestQuery";
        const string path = $"/v1/apps/{appId}/query?query={query}";
        var clientNameCaptor = new ArgumentCaptor<string>();

        var handler = new TestClientHandler();
        var client = new HttpClient(handler)
        {
            BaseAddress = new Uri("http://localhost:8080")
        };
        var factory = A.Fake<IHttpClientFactory>();
        A.CallTo(() => factory.CreateClient(clientNameCaptor))
            .Returns(client);
        
        var resolverClient = new AppInsightsResolverClient(factory);

        await resolverClient.Resolve<object>(appId, query);

        clientNameCaptor.Value.Should().BeEquivalentTo("AppInsights");
        handler.Request.Should().NotBeNull();
        handler.Request!.PathAndQuery.Should().BeEquivalentTo(path);
    }
}