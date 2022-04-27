namespace DatasourceGraphApi;

public class ResolverClient
{
    private readonly IHttpClientFactory _clientFactory;

    public ResolverClient(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public T Resolve<T>(string appId, string query) where T : new()
    {
        var client = _clientFactory.CreateClient();
        return new T();
    }
}