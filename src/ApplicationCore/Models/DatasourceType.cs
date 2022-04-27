using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class DatasourceType
{
    [JsonPropertyName("datasourceId")]
    public int Id { get; set; }
    
    [JsonPropertyName("datasourceTypeName")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("datasourceFields")]
    public List<InputField>? Fields { get; set; }
}
