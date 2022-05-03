using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class InputField
{
    [JsonPropertyName("inputFieldId")]
    public int Id { get; set; }
    
    [JsonPropertyName("inputFieldName")]
    public string Name { get; set; } = null!;
    
    [JsonPropertyName("inputFieldType")]
    public string InputType { get; set; } = "text";
    
    [JsonPropertyName("inputFieldRequired")]
    public bool Required { get; set; }

    [JsonPropertyName("inputFieldValue")]
    public string? Value { get; set; }
}
