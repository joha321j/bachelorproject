using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public abstract class InputType
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; } = null!;
}

public class InputSection : InputType
{
    [JsonPropertyName("inputSectionId")]
    public override int Id { get; set; }
    
    [JsonPropertyName("inputSectionName")]
    public override string Name { get; set; } = null!;
    
    [JsonPropertyName("inputSectionChoice")]
    public InputChoice? Choice { get; set; }

    [JsonPropertyName("inputSectionChoicesName")]
    public string ChoicesName { get; set; } = null!;
    
    [JsonPropertyName("inputSectionChoices")]
    public List<InputChoice> Choices { get; set; } = null!;
    
    [JsonPropertyName("inputSectionRequired")]
    public bool Required { get; set; }
}

public class InputChoice : InputType
{
    [JsonPropertyName("inputChoiceId")]
    public override int Id { get; set; }
    
    [JsonPropertyName("inputChoiceName")]
    public override string Name { get; set; } = null!;
    
    [JsonPropertyName("inputChoiceFields")]
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

public class InputField : InputType
{
    [JsonPropertyName("inputFieldId")]
    public override int Id { get; set; }
    
    [JsonPropertyName("inputFieldName")]
    public override string Name { get; set; } = null!;
    
    [JsonPropertyName("inputFieldType")]
    public string InputType { get; set; } = "text";
    
    [JsonPropertyName("inputFieldRequired")]
    public bool Required { get; set; }

    [JsonPropertyName("inputFieldValue")]
    public string? Value { get; set; }
}
