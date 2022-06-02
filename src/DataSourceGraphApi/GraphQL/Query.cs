using ApplicationCore.Models.AppInsights.Events;
using ApplicationCore.Models.AppInsights.Metrics;
using ApplicationCore.Models.AppInsights.Queries;
using ApplicationCore.Models.Youtube;
using DataSourceGraphApi.GraphQL.ResolverClients;

namespace DataSourceGraphApi.GraphQL;

public class Query
{
    private readonly AppInsightsResolverClient _appInsightsResolverClient;
    private readonly YoutubeResolverClient _youtubeResolverClient;

    public Query(AppInsightsResolverClient appInsightsResolverClient, YoutubeResolverClient youtubeResolverClient)
    {
        _appInsightsResolverClient = appInsightsResolverClient;
        _youtubeResolverClient = youtubeResolverClient;
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
    public async Task<List<Search>?> GetSearchResults(string searchKeyword)
    {
        return await _youtubeResolverClient
            .ResolveSearchResult(searchKeyword);
    }

    
}