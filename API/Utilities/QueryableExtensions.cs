using API.Models;
using Core.Entities;

namespace API.Utilities;


internal static class QueryableExtensions
{
    // todo(seaz96): нужно будет это порефакторить
    internal static IQueryable<GostScore> AddFilters(this IQueryable<GostScore> queryable, SearchFilters? filters)
    {
        if (filters is null)
        {
            return queryable;
        }

        return queryable
            .Where(x => filters.CodeOks == null || (x.Gost.CodeOks != null && x.Gost.CodeOks.Contains(filters.CodeOks)))
            .Where(x => filters.AcceptanceYear == null || x.Gost.AcceptanceYear == filters.AcceptanceYear)
            .Where(x => filters.CommissionYear == null || x.Gost.CommissionYear == filters.CommissionYear)
            .Where(x => filters.Author == null || (x.Gost.Author != null && x.Gost.Author.Contains(filters.Author)))
            .Where(x => filters.AcceptedFirstTimeOrReplaced == null || (x.Gost.AcceptedFirstTimeOrReplaced != null && x.Gost.AcceptedFirstTimeOrReplaced.Contains(filters.AcceptedFirstTimeOrReplaced)))
            .Where(x => filters.KeyWords == null || (x.Gost.KeyWords != null && x.Gost.KeyWords.Contains(filters.KeyWords)))
            .Where(x => x.Gost.Harmonization != null && (filters.Harmonization == null || filters.Harmonization.Contains(x.Gost.Harmonization.Value)))
            .Where(x => x.Gost.Status != null && (filters.Status == null || filters.Status.Contains(x.Gost.Status.Value)))
            .Where(x => x.Gost.AdoptionLevel != null && (filters.AdoptionLevel == null || filters.AdoptionLevel.Contains(x.Gost.AdoptionLevel.Value)))
            .Where(x => filters.Changes == null || (x.Gost.Changes != null && x.Gost.Changes.Contains(filters.Changes)))
            .Where(x => filters.Amendments == null || (x.Gost.Amendments != null && x.Gost.Amendments.Contains(filters.Amendments)));
    }
    
    internal static IQueryable<Gost> AddFilters(this IQueryable<Gost> queryable, SearchFilters? filters)
    {
        if (filters is null)
        {
            return queryable;
        }

        return queryable
            .Where(x => filters.CodeOks == null || (x.CodeOks != null && x.CodeOks.Contains(filters.CodeOks)))
            .Where(x => filters.AcceptanceYear == null || x.AcceptanceYear == filters.AcceptanceYear)
            .Where(x => filters.CommissionYear == null || x.CommissionYear == filters.CommissionYear)
            .Where(x => filters.Author == null || (x.Author != null && x.Author.Contains(filters.Author)))
            .Where(x => filters.AcceptedFirstTimeOrReplaced == null || (x.AcceptedFirstTimeOrReplaced != null && x.AcceptedFirstTimeOrReplaced.Contains(filters.AcceptedFirstTimeOrReplaced)))
            .Where(x => filters.KeyWords == null || (x.KeyWords != null && x.KeyWords.Contains(filters.KeyWords)))
            .Where(x => x.Harmonization != null && (filters.Harmonization == null || filters.Harmonization.Contains(x.Harmonization.Value)))
            .Where(x => x.Status != null && (filters.Status == null || filters.Status.Contains(x.Status.Value)))
            .Where(x => x.AdoptionLevel != null && (filters.AdoptionLevel == null || filters.AdoptionLevel.Contains(x.AdoptionLevel.Value)))
            .Where(x => filters.Changes == null || (x.Changes != null && x.Changes.Contains(filters.Changes)))
            .Where(x => filters.Amendments == null || (x.Amendments != null && x.Amendments.Contains(filters.Amendments)));
    }
}