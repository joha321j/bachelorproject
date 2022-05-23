using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace DataSourceGraphApiTest.GraphQL.ResolverClients.AppInsightsResolverClientTests;

public class ResolveQuery : AppInsightsResolverClientTestsBase
{
    [Fact]  
    public async Task UsesCorrectClientWhenGettingQueries()
    {
        const string query = "TestQuery";
        const string path = $"{AppId}/query?query={query}";

        await ResolverClient.ResolveQuery(AppId, query);

        ClientNameCaptor.Should().BeEquivalentTo(ClientName);
        RequestCaptor.RequestUri.Should().NotBeNull();
        RequestCaptor.RequestUri!.PathAndQuery.Should().BeEquivalentTo(BasePath + path);
    }
}