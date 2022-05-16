using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class DataSource
{
    [JsonPropertyName("dataSourceName")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("dataSourceType")]
    public DataSourceType DataSourceType { get; set; } = null!;
}