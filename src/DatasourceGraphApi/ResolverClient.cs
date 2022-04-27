namespace DatasourceGraphApi;

public class ResolverClient
{
    private readonly IHttpClientFactory _clientFactory;

    public ResolverClient(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<T?> Resolve<T>(string appId, string query)
    {
        var path = $"v1/apps/{appId}/query?query={query}";
        var client = _clientFactory.CreateClient("AppInsights");
        return await client.GetFromJsonAsync<T>(path);
    }
}