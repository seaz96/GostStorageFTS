using System.Text.Json.Serialization;

namespace Core.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HarmonizationLevel
{
    Unharmonized = 0,
    Modified = 1,
    Harmonized = 2
}