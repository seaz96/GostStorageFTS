namespace Core.Entities;

public class Index
{
    public Guid Id { get; }
    public Guid WordId { get; set; } 
    public int GostId { get; set; } 
    public int Frequency { get; set; } 
}