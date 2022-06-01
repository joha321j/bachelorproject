using System.Text.Json.Serialization;

namespace ApplicationCore.Models.AppInsights.Queries;

public class QueryResult
{
    [JsonPropertyName("queryResults")]
    public QueryResults QueryResults { get; set; }
}