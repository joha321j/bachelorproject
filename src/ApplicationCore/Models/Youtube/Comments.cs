using System.Text.Json.Serialization;

namespace ApplicationCore.Models.Youtube;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class AuthorChannelId
{
    [JsonPropertyName("value")]
    public int Value;
}

public class Comments
{
    [JsonPropertyName("kind")]
    public string Kind;

    [JsonPropertyName("id")]
    public int Id;

    [JsonPropertyName("snippet")]
    public Snippet Snippet;
}

public class Root
{
    [JsonPropertyName("Comments")]
    public Comments Comments;
}

public class Snippet
{
    [JsonPropertyName("authorDisplayName")]
    public string AuthorDisplayName;

    [JsonPropertyName("authorProfileImageUrl")]
    public string AuthorProfileImageUrl;

    [JsonPropertyName("authorChannelUrl")]
    public string AuthorChannelUrl;

    [JsonPropertyName("authorChannelId")]
    public AuthorChannelId AuthorChannelId;

    [JsonPropertyName("channelId")]
    public int ChannelId;

    [JsonPropertyName("videoId")]
    public int VideoId;

    [JsonPropertyName("textDisplay")]
    public string TextDisplay;

    [JsonPropertyName("textOriginal")]
    public string TextOriginal;

    [JsonPropertyName("parentId")]
    public int ParentId;

    [JsonPropertyName("canRate")]
    public bool CanRate;

    [JsonPropertyName("viewerRating")]
    public string ViewerRating;

    [JsonPropertyName("likeCount")]
    public int LikeCount;

    [JsonPropertyName("moderationStatus")]
    public string ModerationStatus;

    [JsonPropertyName("publishedAt")]
    public string PublishedAt;

    [JsonPropertyName("updatedAt")]
    public string UpdatedAt;
}

