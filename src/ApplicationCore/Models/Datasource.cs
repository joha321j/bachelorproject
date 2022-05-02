using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class Datasource
{
    [JsonPropertyName("datasourceName")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("datasourceType")]
    public DatasourceType DatasourceType { get; set; } = null!;
}