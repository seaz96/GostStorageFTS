using API.Data;
using Core;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Index = Core.Entities.Index;

namespace API.Services;

public class Indexer(DataContext context)
{
    public async Task<bool> TryIndexAsync(int gostId, string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }
        
        var dictionary = Analyzer.AnalyzeText(text);
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            foreach (var kv in dictionary)
            {
                var word = kv.Key;
                var frequency = kv.Value;

                var dbWord = await context.Words.FirstOrDefaultAsync(w => w.Content == word);

                if (dbWord is null)
                {
                    dbWord = new Word(word);
                    await context.Words.AddAsync(dbWord);
                }

                await context.Indexes.AddAsync(new Index(dbWord.Id, gostId, frequency));

                await context.SaveChangesAsync();
            }

            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }
}