using DatasourceGraphApi.GraphQL.ResolverClients;

namespace DatasourceGraphApi.GraphQL;

public class Query
{
    private readonly AppInsightsResolverClient _appInsightsResolverClient;

    public Query(AppInsightsResolverClient appInsightsResolverClient)
    {
        _appInsightsResolverClient = appInsightsResolverClient;
    }

    public async Task<List<Book>?> GetBooks(string appId, string query)
    {
        return await _appInsightsResolverClient.Resolve<List<Book>>(appId, query);
    }
}   