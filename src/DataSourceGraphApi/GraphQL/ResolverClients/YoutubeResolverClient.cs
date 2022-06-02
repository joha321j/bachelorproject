using ApplicationCore.Models.Youtube;

namespace DataSourceGraphApi.GraphQL.ResolverClients;

public class YoutubeResolverClient
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly string _apiKey;

    public YoutubeResolverClient(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _apiKey = "AIzaSyDO2bM_WXSmnCsSnwqIOwepp9NQTXWaYyg";
    }
    
    public async Task<List<Search>?> ResolveSearchResult(string searchKeyword)
    {
        var path = $"search?maxResults=25&q={searchKeyword}&key={_apiKey}";
        var client = _clientFactory.CreateClient("Youtube");
        return await client.GetFromJsonAsync<List<Search>>(path);
    }

}