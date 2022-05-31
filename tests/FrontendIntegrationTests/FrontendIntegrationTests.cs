using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml;
using ApplicationCore.Models;
using ApplicationCore.Models.AppInsights.Queries;
using DataSourceApp.Services;
using DataSourceGraphApi.GraphQL;
using DataSourceGraphApi.GraphQL.ResolverClients;
using Xunit;
using FrontendIntegrationTests;
using FluentAssertions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using HotChocolate.Execution;
using HotChocolate.Language;
using Microsoft.AspNetCore.Mvc.Formatters;
using Moq;
using Newtonsoft.Json;

namespace DataSourceGraphApiIntegrationTests;

public class FrontendIntegrationTests
{
    private HttpService httpService;
    private DocumentNode docNode;
    public FrontendIntegrationTests()
    {
        var factory = new DataSourceGraphApiTestClientFactory();
        var client = factory.CreateClient();
        httpService = new HttpService(client, new Mock<ILocalStorageService>().Object);
    }
    
    [Fact]
    public async Task ResolveQuery_returns_expected_value()
    {
        var request = new
        {
            query = "query($appId: String!, $queryParam: String!){queryResults(appId: $appId, queryParam: $queryParam){tables{columns{name type}rows}}}",
            variables = new { appId = "test", queryParam = "test" }
        };
        
        var result = await httpService.PostAsync<JsonObject>("/graphQL", request);
        var query = result["data"];
        var tables = query["queryResults"]["tables"][0]["columns"].Deserialize<List<Column>>();
        result.Should().NotBeNull();

    }
}