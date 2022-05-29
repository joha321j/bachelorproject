using System;
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
using HotChocolate.Language;
using Moq;

namespace DataSourceGraphApiIntegrationTests;

public class FrontendIntegrationTests
{
    private HttpService httpService;
    private Uri baseAddress;
    public FrontendIntegrationTests()
    {
        var factory = new DataSourceGraphApiTestClientFactory();
        var client = factory.CreateClient();
        baseAddress = client.BaseAddress;
        httpService = new HttpService(client, new Mock<ILocalStorageService>().Object);
    }
    
    [Fact]
    public async Task ResolveQuery_returns_expected_value()
    {
        string query = "Hello"; 
        
        GraphQLRequest request = new GraphQLRequest
        {
            Query = query
        };
        httpService.PostAsync(baseAddress.ToString(), );
    }

   
}