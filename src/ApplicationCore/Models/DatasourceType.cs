using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApplicationCore.Models;

[TypeConverter(typeof(DatasourceTypeConverter))]
public class DatasourceType
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

[TypeConverter(typeof(DatasourceTypeConverter))]
public class DatasourceTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }

        return base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string)
        {
            return JsonSerializer.Deserialize<DatasourceType>(value.ToString()
                                                              ?? throw new InvalidOperationException());
        }
        
        return base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        return destinationType == typeof(string) 
            ? JsonSerializer.Serialize(value)
            : base.ConvertTo(context, culture, value, destinationType);
    }
}