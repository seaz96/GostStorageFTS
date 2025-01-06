using API.Data;
using API.Models;
using API.Utilities;
using Core.Analyzer;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class Search(DataContext context) : ISearch
{
    private const int DefaultLimit = 10;
    private const int DefaultOffset = 0;
    
    public async Task<List<SearchEntity>> SearchAsync(SearchQuery query)
    {
        if (string.IsNullOrEmpty(query.Text))
        {
            return await SearchAllAsync(query).ConfigureAwait(false);
        }
        
        var words = query.Text.TokenizeText().Filter().Stem().ToHashSet();

        return await context.Words
            .Where(w => words.Contains(w.Content))
            .Join(
                context.Indexes,
                w => w.Id,
                i => i.WordId,
                (w, i) => i)
            .Join(
                context.Gosts,
                i => i.GostId,
                g => g.Id,
                (index, gost) => new
                {
                    index.Frequency,
                    index.WordId,
                    Gost = gost
                })
            .GroupBy(group => group.Gost)
            .Select(group => new GostScore
            {
                Gost = group.Key,
                Score = (group.Count() / (double)words.Count + group.Sum(
                    i => i.Frequency) / (double)group.Key.IndexedWordsCount!) / 2.0d
            })
            .AddFilters(query.SearchFilters)
            .OrderByDescending(x => x.Score)
            .Select(x => new SearchEntity(x.Gost.Id, x.Gost.CodeOks, x.Gost.Designation, x.Gost.FullName, x.Score))
            .Skip(query.Offset ?? 0)
            .Take(query.Limit ?? 10)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<int> CountAsync(SearchQuery query)
    {
        if (string.IsNullOrEmpty(query.Text))
        {
            return await SearchAllCountAsync(query).ConfigureAwait(false);
        }
        
        var words = query.Text.TokenizeText().Filter().Stem().ToHashSet();

        return await context.Words
            .Where(w => words.Contains(w.Content))
            .Join(
                context.Indexes,
                w => w.Id,
                i => i.WordId,
                (w, i) => i)
            .Join(
                context.Gosts,
                i => i.GostId,
                g => g.Id,
                (index, gost) => new
                {
                    index.Frequency,
                    index.WordId,
                    Gost = gost
                })
            .GroupBy(group => group.Gost)
            .Select(group => new GostScore
            {
                Gost = group.Key,
                Score = (group.Count() / (double)words.Count + group.Sum(
                    i => i.Frequency) / (double)group.Key.IndexedWordsCount!) / 2.0d
            })
            .AddFilters(query.SearchFilters)
            .CountAsync();
    }

    public Task<List<SearchEntity>> SearchAllAsync(SearchQuery query)
    {
        return context.Gosts
            .AddFilters(query.SearchFilters)
            .OrderBy(x => x.CodeOks)
            .Skip(query.Offset ?? DefaultOffset)
            .Take(query.Limit ?? DefaultLimit)
            .Select(x => new SearchEntity(x.Id, x.CodeOks, x.Designation, x.FullName, 1))
            .ToListAsync();
    }
    
    public Task<int> SearchAllCountAsync(SearchQuery query)
    {
        return context.Gosts
            .AddFilters(query.SearchFilters)
            .OrderBy(x => x.CodeOks)
            .CountAsync();
    }
}