using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Models;
using DataSourceApp.ApiContracts;
using Xunit;
using FluentAssertions;

namespace DataSourceGraphApiIntegrationTests;

public class FrontendIntegrationTests : ClientSetup
{

    public FrontendIntegrationTests() : base(collection => collection)
    {
    }
    
    [Fact]
    public async Task AsList_returns_list_when_asked_for_single_item()
    {
        var dataSource = new DataSource
        {
            Name = "TestName",
            DataSourceType = new DataSourceType
            {
                Id = 1,
                Name = "Azure App Insights",
                Fields = new List<InputSection>()
            }
        };

        var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = new StringContent(JsonSerializer.Serialize(dataSource));

        var client = new FakeHttpClient(response);
        
        
        var request = new Request
        {
            RequestMethod = async () => await Task.FromResult(response)
        };
        
    }

   
}