using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Models;
using DataSourceApp.Services;
using DataSourceGraphApi.GraphQL.ResolverClients;
using Xunit;
using FrontendIntegrationTests;
using FluentAssertions;

namespace DataSourceGraphApiIntegrationTests;

public class FrontendIntegrationTests
{
    private AppInsightsResolverClient insight;
    public FrontendIntegrationTests()
    {
    }
    
    [Fact]
    public async Task ResolveQuery_returns_expected_value()
    {
        
        /*var dataSource = new DataSource
        {
            Name = "TestName",
            DataSourceType = new DataSourceType
            {
                Id = 1,
                Name = "Azure App Insights",
                Fields = new List<InputSection>()
            }
        };*/
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = new StringContent(JsonSerializer.Serialize(insight));

        var client = new HttpClient();
        
        HttpService service = new HttpService(client, new LocalStorageService(null));

        var sut = service.PostAsync<AppInsightsResolverClient>(client.BaseAddress.AbsoluteUri, insight);

        var result = await sut.Result.ResolveQuery("1", "Hej");

        result.Should().BeEquivalentTo("Hej");
    }

   
}