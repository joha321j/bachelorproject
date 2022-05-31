using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Models.AppInsights.Queries;
using DataSourceApp.Services;
using FluentAssertions;
using HotChocolate.Language;
using Moq;
using Xunit;
using QueryResult = ApplicationCore.Models.AppInsights.Queries.QueryResult;

namespace FrontendIntegrationTests;

public class FrontendIntegrationTests
{
    private readonly HttpService _httpService;
    public FrontendIntegrationTests()
    {
        var factory = new DataSourceGraphApiTestClientFactory();
        var client = factory.CreateClient();
        _httpService = new HttpService(client, new Mock<ILocalStorageService>().Object);
    }

    [Fact]
    public async Task ResolveQuery_returns_expected_value()
    {
        var request = new
        {
            query = "query($appId: String!, $queryParam: String!){queryResults(appId: $appId, queryParam: $queryParam){tables{columns{name type}rows}}}",
            variables = new { appId = "test", queryParam = "test" }
        };

        var result = await _httpService.PostAsync<DataPackage<QueryResult>>("/graphQL", request);

        result.Should().NotBeNull();
    }
}