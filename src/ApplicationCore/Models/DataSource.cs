using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class DataSource
{
    [JsonPropertyName("datasourceName")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("datasourceType")]
    public DataSourceType DataSourceType { get; set; } = null!;
}