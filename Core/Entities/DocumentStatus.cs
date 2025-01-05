using System.Text.Json.Serialization;

namespace Core.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentStatus
{
    Valid = 0,
    Canceled = 1,
    Replaced = 2,
    Inactive = 3
}