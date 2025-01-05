using System.Text.Json.Serialization;

namespace Core.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AdoptionLevel
{
    International = 0,
    Foreign = 1,
    Regional = 2,
    Organizational = 3,
    National = 4,
    Interstate = 5
}