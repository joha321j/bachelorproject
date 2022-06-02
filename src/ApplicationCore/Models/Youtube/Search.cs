using System.Text.Json.Serialization;

namespace ApplicationCore.Models.Youtube;

public class Id
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }

    [JsonPropertyName("videoId")]
    public string VideoId { get; set; }

    [JsonPropertyName("channelId")]
    public string ChannelId { get; set; }

    [JsonPropertyName("playlistId")]
    public string PlaylistId { get; set; }
}

public class SearchResults
{
    [JsonPropertyName("searchResults")]
    public List<Search> Search { get; set; }
}

public class Search
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }

    [JsonPropertyName("etag")]
    public string Etag { get; set; }

    [JsonPropertyName("id")]
    public Id Id { get; set; }

    [JsonPropertyName("snippet")]
    public Snippet Snippet { get; set; }
}

public class Snippet
{
    [JsonPropertyName("publishedAt")]
    public string PublishedAt { get; set; }

    [JsonPropertyName("channelId")]
    public string ChannelId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("thumbnails")]
    public Thumbnails Thumbnails { get; set; }

    [JsonPropertyName("channelTitle")]
    public string ChannelTitle { get; set; }

    [JsonPropertyName("liveBroadcastContent")]
    public string LiveBroadcastContent { get; set; }
}

public class Thumbnails
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}



