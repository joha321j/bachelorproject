namespace DatasourceGraphApi.GraphQL;

public class Query
{
    private readonly AppInsightsResolverClient _client;

    public Query(AppInsightsResolverClient client)
    {
        _client = client;
    }

    public async Task<List<Book>?> GetBooks(string appId)
    {
        return await _client.Resolve<List<Book>>(appId, "atoeuha");
    }
}   