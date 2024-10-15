namespace Core.Entities;

public class Index
{
    public Guid Id { get; set; }
    public Guid WordId { get; set; }
    public Guid GostId { get; set; }
    public int Frequency { get; set; }
}