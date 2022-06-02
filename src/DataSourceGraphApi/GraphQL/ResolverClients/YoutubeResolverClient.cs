using ApplicationCore.Models.Youtube;

namespace DataSourceGraphApi.GraphQL.ResolverClients;

public class YoutubeResolverClient
{
    private readonly IHttpClientFactory _clientFactory;
    private IConfiguration _configuration;

    public YoutubeResolverClient(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
    }
    
    public async Task<List<Search>?> ResolveSearchResult(string searchKeyword)
    {
        var path = $"search?maxResults=25&q={searchKeyword}&key={_configuration.GetValue<string>("ApiKey")}";
        var client = _clientFactory.CreateClient("Youtube");
        return await client.GetFromJsonAsync<List<Search>>(path);
    }

}