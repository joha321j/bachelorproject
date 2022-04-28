using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class DatasourceTypeSelection : InputTypeSelection
{
    [JsonPropertyName("datasourceId")]
    public override int Id { get; set; }
    
    [JsonPropertyName("datasourceTypeName")]
    public override string Name { get; set; } = null!;

    [JsonPropertyName("datasourceFields")]
    public List<InputSection>? Fields { get; set; }
}
