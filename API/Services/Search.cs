using API.Data;
using API.Models;
using Core;
using Core.Analyzer;
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
                (index, gost) => new SearchEntity
                    {
                        Id = gost.Id,
                        CodeOKS = gost.CodeOKS,
                        Designation = gost.Designation,
                        FullName = gost.FullName,
                        Score = (index.Coverage / (double)words.Length + index.Frequency / (double)gost.IndexedWordsCount) / 2.0d
                    })
            .OrderByDescending(searchEntity => searchEntity.Score)
            .Take(query.Take)
            .Skip(query.Skip)
            .ToListAsync();
    }
}