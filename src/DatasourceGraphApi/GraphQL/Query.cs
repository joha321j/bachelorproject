using ApplicationCore.Models.AppInsights.Events;
using ApplicationCore.Models.AppInsights.Metrics;
using ApplicationCore.Models.AppInsights.Queries;
using DatasourceGraphApi.GraphQL.ResolverClients;

namespace DatasourceGraphApi.GraphQL;

public class Query
{
    private readonly AppInsightsResolverClient _appInsightsResolverClient;

    public Query(AppInsightsResolverClient appInsightsResolverClient)
    {
        _appInsightsResolverClient = appInsightsResolverClient;
    }

    public async Task<QueryResults?> GetQueryResults(string appId, string queryParam)
    {
        return await _appInsightsResolverClient.ResolveQuery(appId, queryParam);
    }

    public async Task<MetricsResultsItem?> GetMetricsResultsItem(
        string appId,
        string metricId,
        List<KeyValuePair<string, string>>? parameters)
    {
        return await _appInsightsResolverClient
            .ResolveMetrics(appId, metricId, parameters);
    }

    public async Task<EventsResults?> GetEventsResult(
        string appId,
        string eventType,
        string eventId,
        TimeSpan timeSpan = default)
    {
        return await _appInsightsResolverClient
            .ResolveEvents(appId, eventType, eventId, timeSpan);
    }
}   