using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace API.Models;

public record SearchFilters(
    string? CodeOks,
    [Range(1000, 9999)] int? AcceptanceYear,
    [Range(1000, 9999)] int? CommissionYear,
    string? Author,
    string? AcceptedFirstTimeOrReplaced,
    string? KeyWords,
    DocAdoptionLevels? AdoptionLevel,
    DocStatuses? Status,
    HarmonizationLevels? Harmonization
);