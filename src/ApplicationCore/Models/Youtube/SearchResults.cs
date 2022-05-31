using System.Text.Json.Serialization;

namespace ApplicationCore.Models.Youtube;

public class Id
{
    [JsonPropertyName("kind")]
    public string Kind;

    [JsonPropertyName("videoId")]
    public string VideoId;

    [JsonPropertyName("channelId")]
    public string ChannelId;

    [JsonPropertyName("playlistId")]
    public string PlaylistId;
}

public class Root
{
    [JsonPropertyName("SearchResults")]
    public SearchResults SearchResults;
}

public class SearchResults
{
    [JsonPropertyName("kind")]
    public string Kind;

    [JsonPropertyName("etag")]
    public string Etag;

    [JsonPropertyName("id")]
    public Id Id;

    [JsonPropertyName("snippet")]
    public Snippet Snippet;
}

public class Snippet
{
    [JsonPropertyName("publishedAt")]
    public string PublishedAt;

    [JsonPropertyName("channelId")]
    public string ChannelId;

    [JsonPropertyName("title")]
    public string Title;

    [JsonPropertyName("description")]
    public string Description;

    [JsonPropertyName("thumbnails")]
    public Thumbnails Thumbnails;

    [JsonPropertyName("channelTitle")]
    public string ChannelTitle;

    [JsonPropertyName("liveBroadcastContent")]
    public string LiveBroadcastContent;
}

public class Thumbnails
{
    [JsonPropertyName("url")]
    public string Url;

    [JsonPropertyName("width")]
    public int Width;

    [JsonPropertyName("height")]
    public int Height;
}



