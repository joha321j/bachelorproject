using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class DataSourceType
{
    [JsonPropertyName("datasourceTypeId")]
    public int Id { get; set; }
    
    [JsonPropertyName("datasourceTypeName")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("datasourceTypeFields")]
    public List<InputSection>? Fields { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}