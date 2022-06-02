using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Models.Youtube;
using DataSourceApp.Services;
using FluentAssertions;
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
    public async Task AppInsights_ResolveQuery_returns_expected_value()
    {
        var request = new
        {
            query = "query($appId: String!, $queryParam: String!){queryResults(appId: $appId, queryParam: $queryParam){tables{columns{name type}rows}}}",
            variables = new { appId = "test", queryParam = "test" }
        };

        var result = await _httpService.PostAsync<DataPackage<QueryResult>>("/graphQL", request);

        var columnName = result.Data.QueryResults.Tables[0].Columns[0].Name;

        result.Should().NotBeNull();

        columnName.Should().BeEquivalentTo("TestColumn");
    }
    
    [Fact]
    public async Task Youtube_SearchResults_returns_expected_value()
    {
        var request = new
        {
            query = "query{searchResults(searchKeyword: \"Surfing\"){kind}}"
        };

        var result = await _httpService.PostAsync<DataPackage<SearchResults>>("/graphQL", request);

        
        var kind = result.Data.Search.FirstOrDefault().Kind;

        result.Should().NotBeNull();
        kind.Should().Be("TestKind");

    }
}