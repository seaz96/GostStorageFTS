namespace API.Models;

public record SearchQuery(string? Text, SearchFilters? SearchFilters, int Limit = 10, int Offset = 0);