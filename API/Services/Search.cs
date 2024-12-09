using API.Data;
using API.Models;
using Core.Analyzer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Services;

namespace API.Services;

public class Search(DataContext context) : ISearch
{
    //todo(azanov.n): мне кажется с такими запросами я превращаюсь в trainee
    //todo(azanov.n): нужно научиться обрабатывать поиск с фильтрами и null text
    public async Task<List<SearchEntity>> SearchAsync(SearchQuery query)
    {
        if (query.Text is null)
        {
            return await SearchAllAsync(query).ConfigureAwait(false);
        }
        
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
                    Score = (index.Coverage / (double)words.Length + index.Frequency / (double)gost.IndexedWordsCount!) / 2.0d
                })
            .AddFilters(query.SearchFilters)
            .OrderByDescending(x => x.Score)
            .Select(x => new SearchEntity(x.Gost.Id, x.Gost.CodeOks, x.Gost.Designation, x.Gost.FullName, x.Score))
            .Skip(query.Offset ?? 0)
            .Take(query.Limit ?? 10)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public Task<List<SearchEntity>> SearchAllAsync(SearchQuery query)
    {
        return context.Gosts
            .OrderBy(x => x.CodeOks)
            .Skip(query.Offset ?? 0)
            .Take(query.Limit ?? 10)
            .Select(x => new SearchEntity(x.Id, x.CodeOks, x.Designation, x.FullName, 1))
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
            .Where(x => filters.CodeOks == null || (x.Gost.CodeOks != null && x.Gost.CodeOks.Contains(filters.CodeOks)))
            .Where(x => filters.AcceptanceYear == null || x.Gost.AcceptanceYear == filters.AcceptanceYear)
            .Where(x => filters.CommissionYear == null || x.Gost.CommissionYear == filters.CommissionYear)
            .Where(x => filters.Author == null || (x.Gost.Author != null && x.Gost.Author.Contains(filters.Author)))
            .Where(x => filters.AcceptedFirstTimeOrReplaced == null || (x.Gost.AcceptedFirstTimeOrReplaced != null && x.Gost.AcceptedFirstTimeOrReplaced.Contains(filters.AcceptedFirstTimeOrReplaced)))
            .Where(x => filters.KeyWords == null || (x.Gost.KeyWords != null && x.Gost.KeyWords.Contains(filters.KeyWords)))
            .Where(x => filters.Harmonization == null || x.Gost.Harmonization == filters.Harmonization)
            .Where(x => filters.Status == null || x.Gost.Status == filters.Status)
            .Where(x => filters.AdoptionLevel == null || x.Gost.AdoptionLevel == filters.AdoptionLevel);
    }
}