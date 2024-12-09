namespace API.Models;

public record SearchQuery(string? Text, SearchFilters? SearchFilters, int? Limit, int? Offset);