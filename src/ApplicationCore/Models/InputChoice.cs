using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class InputChoice
{
    [JsonPropertyName("inputChoiceId")]
    public int Id { get; set; }
    
    [JsonPropertyName("inputChoiceName")]
    public string Name { get; set; } = null!;
    
    [JsonPropertyName("inputChoiceFields")]
    public List<InputField> Fields { get; set; } = null!;

    public override string ToString()
    {
        var result = "";
        foreach (var field in Fields)
        {
            result += $"{field.Name}: {field.Value}, Required: {field.Required}, InputTypeSelection: {field.InputType}";
        }

        return result;
    }
}