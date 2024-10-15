namespace API.Models;

public record SearchQuery
{
    public string Text { get; init; }
    public int Take { get; init; }
    public int Skip { get; init; }
}