using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace DataSourceGraphApiTest.GraphQL.ResolverClients.AppInsightsResolverClientTests;

public class ResolveMetrics : AppInsightsResolverClientTestsBase
{
    [Theory]
    [MemberData(nameof(PathTestData))]
    public async Task ConstructsCorrectPathWhenGettingMetrics(
        List<KeyValuePair<string, string>> parameters,
        string path)
    {
        await ResolverClient.ResolveMetrics(AppId, MetricId, parameters);

        ClientNameCaptor.Should().BeEquivalentTo(ClientName);
        RequestCaptor.RequestUri.Should().NotBeNull();
        RequestCaptor.RequestUri!.PathAndQuery.Should().BeEquivalentTo(BasePath + path);
    }

    private static IEnumerable<object[]> PathTestData()
    {
        const string basePath = $"{AppId}/metrics/{MetricId}";
        var parameters = new List<KeyValuePair<string, string>>
        {
            new("firstParameter", "Value"),
            new("anotherParameter", "anotherValue")
        };
        
        yield return new object[]
        {
            new List<KeyValuePair<string, string>>(),
            basePath
        };
        yield return new object[]
        {
            new List<KeyValuePair<string, string>>
            {
                parameters[0]
            },
            basePath + $"?{parameters[0].Key}={parameters[0].Value}"
        };
        yield return new object[]
        {
            parameters,
            basePath +
            $"?{parameters[0].Key}={parameters[0].Value}" +
            $"&{parameters[1].Key}={parameters[1].Value}"
        };
    }
}