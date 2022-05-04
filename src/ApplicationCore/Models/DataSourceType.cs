using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class DataSourceType
{
    [JsonPropertyName("dataSourceTypeId")]
    public int Id { get; set; }
    
    [JsonPropertyName("dataSourceTypeName")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("dataSourceTypeFields")]
    public List<InputSection>? Fields { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}