namespace API.Models;

public record SearchQuery(string? Text, SearchFilters? SearchFilters, int Take = 10, int Skip = 0);