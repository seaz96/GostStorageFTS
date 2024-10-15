namespace Core.Entities;

public class Word(string word)
{
    public Guid Id { get; set; }
    public string Content { get; init; } = word;
}