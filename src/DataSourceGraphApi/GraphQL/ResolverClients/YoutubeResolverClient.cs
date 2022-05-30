namespace DataSourceGraphApi.GraphQL.ResolverClients;

public class YoutubeResolverClient
{
    private readonly IHttpClientFactory _clientFactory;

    public YoutubeResolverClient(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

}