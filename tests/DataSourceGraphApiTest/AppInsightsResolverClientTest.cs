using System;
using System.Net.Http;
using System.Threading.Tasks;
using DataFakingLibrary;
using DatasourceGraphApi;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace DataSourceGraphApiTest;

public class AppInsightsResolverClientTest
{
    [Fact]
    public async Task Test1()
    {

        var client = new HttpClient(new FakeInsightHandler())
        {
            BaseAddress = new Uri("https://localhost:8080")
        };
        var factory = A.Fake<IHttpClientFactory>();
        A.CallTo(() => factory.CreateClient(A<string>.Ignored))
            .Returns(client);

        var resolverClient = new AppInsightsResolverClient(factory);

        var result = await resolverClient.Resolve<object>("test", "test");

        result.Should();
    }
}