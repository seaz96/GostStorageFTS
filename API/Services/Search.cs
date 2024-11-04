using API.Data;
using API.Models;
using Core.Analyzer;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class Search(DataContext context) : ISearch
{
    public async Task<List<SearchEntity>> SearchAsync(SearchQuery query)
    {
        var words = query.Text.TokenizeText().Filter().Stem().ToArray();
        
        var dbWords = context.Words.Where(w => words.Contains(w.Content)).Select(w => w.Id);

        var dbIndexes = context.Indexes.Where(i => dbWords.Contains(i.WordId));

        return await dbIndexes
            .GroupBy(e => e.GostId)
            .Select(group => new
            {
                GostId = group.Key,
                Frequency = group.Sum(i => i.Frequency),
                Coverage = group.Count()
            })
            .Join(
                context.Gosts,
                i => i.GostId,
                g => g.Id,
                (index, gost) => new GostScore
                    {
                        Gost = gost,
                        Score = (index.Coverage / (double)words.Length + index.Frequency / (double)gost.IndexedWordsCount) / 2.0d
                    })
            .AddFilters(query.SearchFilters)
            .OrderByDescending(x => x.Score)
            .Select(x => new SearchEntity
            {
                Id = x.Gost.Id,
                CodeOks = x.Gost.CodeOks,
                Designation = x.Gost.Designation,
                FullName = x.Gost.FullName,
                Score = x.Score
            })
            .Skip(query.Skip)
            .Take(query.Take)
            .ToListAsync();
    }

    public Task<List<SearchEntity>> SearchAllAsync(SearchQuery query)
    {
        return context.Gosts
            .OrderBy(x => x.CodeOks)
            .AddFilters(query.SearchFilters)
            .Skip(query.Skip)
            .Take(query.Take)
            .Select(x => new SearchEntity
            {
                Id = x.Id,
                CodeOks = x.CodeOks,
                Designation = x.Designation,
                FullName = x.FullName,
                Score = 5
            })
            .ToListAsync();
    }
}

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
            .Where(x => filters.CodeOks != null && x.Gost.CodeOks != null && x.Gost.CodeOks.Contains(filters.CodeOks))
            .Where(x => x.Gost.AcceptanceYear == filters.AcceptanceYear)
            .Where(x => x.Gost.CommissionYear == filters.CommissionYear)
            .Where(x => filters.Author != null && x.Gost.Author != null && x.Gost.Author.Contains(filters.Author))
            .Where(x => filters.AcceptedFirstTimeOrReplaced != null && x.Gost.AcceptedFirstTimeOrReplaced != null && x.Gost.AcceptedFirstTimeOrReplaced.Contains(filters.AcceptedFirstTimeOrReplaced))
            .Where(x => filters.KeyWords != null && x.Gost.KeyWords != null && x.Gost.KeyWords.Contains(filters.KeyWords))
            .Where(x => x.Gost.Harmonization == filters.Harmonization)
            .Where(x => x.Gost.Status == filters.Status)
            .Where(x => x.Gost.AdoptionLevel == filters.AdoptionLevel);
    }
    
    internal static IQueryable<Gost> AddFilters(this IQueryable<Gost> queryable, SearchFilters? filters)
    {
        if (filters is null)
        {
            return queryable;
        }

        return queryable
            .Where(x => filters.CodeOks != null && x.CodeOks != null && x.CodeOks.Contains(filters.CodeOks))
            .Where(x => x.AcceptanceYear == filters.AcceptanceYear)
            .Where(x => x.CommissionYear == filters.CommissionYear)
            .Where(x => filters.Author != null && x.Author != null && x.Author.Contains(filters.Author))
            .Where(x => filters.AcceptedFirstTimeOrReplaced != null && x.AcceptedFirstTimeOrReplaced != null && x.AcceptedFirstTimeOrReplaced.Contains(filters.AcceptedFirstTimeOrReplaced))
            .Where(x => filters.KeyWords != null && x.KeyWords != null && x.KeyWords.Contains(filters.KeyWords))
            .Where(x => x.Harmonization == filters.Harmonization)
            .Where(x => x.Status == filters.Status)
            .Where(x => x.AdoptionLevel == filters.AdoptionLevel);
    }
}

internal class GostScore
{
    public Gost Gost { get; set; }
    public double Score { get; set; }

}