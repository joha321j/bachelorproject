using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class InputSection
{
    [JsonPropertyName("inputSectionId")]
    public int Id { get; set; }
    
    [JsonPropertyName("inputSectionName")]
    public string Name { get; set; } = null!;
    
    [JsonPropertyName("inputSectionChoice")]
    public InputChoice? Choice { get; set; }

    [JsonPropertyName("inputSectionChoicesName")]
    public string ChoicesName { get; set; } = null!;
    
    [JsonPropertyName("inputSectionChoices")]
    public List<InputChoice> Choices { get; set; } = null!;
    
    [JsonPropertyName("inputSectionRequired")]
    public bool Required { get; set; }
}