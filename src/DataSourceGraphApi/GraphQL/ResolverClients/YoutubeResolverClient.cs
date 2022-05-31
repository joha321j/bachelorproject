using ApplicationCore.Models.AppInsights.Queries;
using ApplicationCore.Models.Youtube;

namespace DataSourceGraphApi.GraphQL.ResolverClients;

public class YoutubeResolverClient
{
    private readonly IHttpClientFactory _clientFactory;

    public YoutubeResolverClient(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    
    public async Task<Comments?> ResolveComment(string query)
    {
        var path = $"{appId}/query?query={query}";
        var client = _clientFactory.CreateClient("Youtube");
        return await client.GetFromJsonAsync<Comments>(path);
    }

}