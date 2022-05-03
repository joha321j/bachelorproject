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
        return await _appInsightsResolverClient.Resolve(appId, queryParam);
    }

    public async Task<MetricsResultsItem?> GetMetricResults(
        string appId,
        string metricId,
        List<KeyValuePair<string, string>> parameters)
    {
        return await _appInsightsResolverClient
            .Resolve(appId, metricId, parameters);
    }
}   