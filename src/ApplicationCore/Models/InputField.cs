using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class InputRegion
{
    public string Name { get; set; } = null!;
    public InputChoice? Choice { get; set; }
    public List<InputChoice> Choices { get; set; } = null!;
    public bool Required { get; set; }
}

public class InputChoice
{
    public string Name { get; set; } = null!;
    public List<InputField> Fields { get; set; } = null!;

    public override string ToString()
    {
        var result = "";
        foreach (var field in Fields)
        {
            result += $"{field.Name}: {field.Value}, Required: {field.Required}, InputType: {field.InputType}";
        }

        return result;
    }
}

public class InputField
{
    [JsonPropertyName("inputFieldName")]
    public string Name { get; set; } = null!;
    
    [JsonPropertyName("inputFieldType")]
    public string InputType { get; set; } = null!;
    
    [JsonPropertyName("inputFieldRequired")]
    public bool Required { get; set; }

    [JsonPropertyName("inputFieldValue")]
    public string? Value { get; set; }
}
