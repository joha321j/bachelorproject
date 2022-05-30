using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using ApplicationCore.Models;
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
    private Uri baseAddress;
    private DocumentNode docNode;
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
        var query = @"
                query queryResults(appId: ""asdasd"", queryParam: ""asdasd"") {
                    tables{
                       columns{
                           name
                               }
                           }
                 }";

        var result = await httpService.PostAsync<object>("/graphQL", query);
        result.Should().NotBeNull();

    }
}