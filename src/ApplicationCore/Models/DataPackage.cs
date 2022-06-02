using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

public class DataPackage<T>
{
    [JsonPropertyName("data")]
    public T Data { get; set; }
}