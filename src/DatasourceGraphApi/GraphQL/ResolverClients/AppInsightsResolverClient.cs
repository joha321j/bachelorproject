using ApplicationCore.Models.AppInsights.Metrics;
using ApplicationCore.Models.AppInsights.Queries;

namespace DatasourceGraphApi.GraphQL.ResolverClients;

public class AppInsightsResolverClient
{
    private readonly IHttpClientFactory _clientFactory;

    public AppInsightsResolverClient(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<QueryResults?> Resolve(string appId, string query)
    {
        var path = $"v1/apps/{appId}/query?query={query}";
        var client = _clientFactory.CreateClient("AppInsights");

        return await client.GetFromJsonAsync<QueryResults>(path);
    }

    public async Task<MetricsResultsItem?> Resolve(
        string appId,
        string metricId,
        List<KeyValuePair<string, string>> parameters)
    {
        var path = CreatePath(appId, metricId, parameters);
        var client = _clientFactory.CreateClient("AppInsights");
        return await client.GetFromJsonAsync<MetricsResultsItem>(path);
    }

    private static string CreatePath(string appId, string metricId, List<KeyValuePair<string, string>> parameters)
    {
        var path = $"v1/apps/{appId}/metrics/{metricId}";

        var first = true;
        foreach (KeyValuePair<string, string> pair in parameters)
        {
            if (first)
            {
                path += "?";
                first = false;
            }
            else
            {
                path += "&";
            }

            path += $"{pair.Key}={pair.Value}";
        }

        return path;
    }
}