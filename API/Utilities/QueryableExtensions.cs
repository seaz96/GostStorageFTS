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
            .Where(x => filters.Harmonization == null || x.Gost.Harmonization == filters.Harmonization)
            .Where(x => filters.Status == null || x.Gost.Status == filters.Status)
            .Where(x => filters.AdoptionLevel == null || x.Gost.AdoptionLevel == filters.AdoptionLevel)
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
            .Where(x => filters.Harmonization == null || x.Harmonization == filters.Harmonization)
            .Where(x => filters.Status == null || x.Status == filters.Status)
            .Where(x => filters.AdoptionLevel == null || x.AdoptionLevel == filters.AdoptionLevel)
            .Where(x => filters.Changes == null || (x.Changes != null && x.Changes.Contains(filters.Changes)))
            .Where(x => filters.Amendments == null || (x.Amendments != null && x.Amendments.Contains(filters.Amendments)));
    }
}