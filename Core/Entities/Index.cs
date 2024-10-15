namespace Core.Entities;

public class Index(Guid wordId, int gostId, int frequency)
{
    public Guid Id { get; }
    public Guid WordId { get; init; } = wordId;
    public int GostId { get; init; } = gostId;
    public int Frequency { get; init; } = frequency;
}