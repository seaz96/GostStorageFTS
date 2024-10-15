namespace API.Models;

public class SearchEntity
{
    public int Id { get; set; }
    public string? CodeOKS { get; set; }
    public string Designation { get; set; }
    public string FullName { get; set; }
    public double Score { get; set; }
}