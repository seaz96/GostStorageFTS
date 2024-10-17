using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Gost
{
    public int Id { get; set; }
    public string Designation { get; set; }
    public string FullName { get; set; }
    public string? CodeOks { get; set; }
    public string? ActivityField { get; set; }
    [Range(1000, 9999)] public int? AcceptanceYear { get; set; }
    [Range(1000, 9999)] public int? CommissionYear { get; set; }
    public string? Author { get; set; }
    public string? AcceptedFirstTimeOrReplaced { get; set; }
    public string? Content { get; set; }
    public string? KeyWords { get; set; }
    public string? ApplicationArea { get; set; }
    public DocAdoptionLevels? AdoptionLevel { get; set; }
    public string? Changes { get; set; }
    public string? Amendments { get; set; }
    public DocStatuses Status { get; set; }
    public HarmonizationLevels? Harmonization { get; set; }
    public int IndexedWordsCount { get; set; }
}

public enum DocAdoptionLevels
{
    International = 0,
    Foreign = 1,
    Regional = 2,
    Organizational = 3,
    National = 4,
    Interstate = 5
}

public enum DocStatuses
{
    Valid = 0,
    Canceled = 1,
    Replaced = 2,
    Inactive = 3
}

public enum HarmonizationLevels
{
    Unharmonized = 0,
    Modified = 1,
    Harmonized = 2
}