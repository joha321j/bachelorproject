using System.Xml;
using ApplicationCore.Models.AppInsights.Events;
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
        var path = $"{appId}/query?query={query}";
        var client = _clientFactory.CreateClient("AppInsights");
        return await client.GetFromJsonAsync<QueryResults>(path);
    }

    public async Task<MetricsResultsItem?> Resolve(
        string appId,
        string metricId,
        List<KeyValuePair<string, string>>? parameters)
    {
        var path = CreateMetricsPath(appId, metricId, parameters);
        var client = _clientFactory.CreateClient("AppInsights");
        return await client.GetFromJsonAsync<MetricsResultsItem>(path);
    }

    private static string CreateMetricsPath(
        string appId,
        string metricId,
        List<KeyValuePair<string, string>>? parameters)
    {
        var path = $"{appId}/metrics/{metricId}";

        var first = true;
        if (parameters == null) return path;
        
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

    public async Task<EventsResults?> Resolve(
        string appId,
        string eventType,
        string eventId,
        TimeSpan timeSpan = default)
    {
        var path = $"{appId}/events/{eventType}/{eventId}";
        if (timeSpan != TimeSpan.Zero)
        {
            path += $"?timespan={XmlConvert.ToString(timeSpan)}";
        }

        var client = _clientFactory.CreateClient("AppInsights");
        return await client.GetFromJsonAsync<EventsResults>(path);
    }
}