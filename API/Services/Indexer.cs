using System.Text;
using API.Data;
using API.Models;
using Core;
using Core.Analyzer;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Index = Core.Entities.Index;

namespace API.Services;

public class Indexer(DataContext context, IGostsService gostsService) : IIndexer
{
    public async Task<bool> TryIndexAsync(IndexRequest request)
    {
        var gostId = request.Document.Id;
        var text = new StringBuilder(request.Text + ' ')
            .Append(request.Document.Designation + ' ')
            .Append(request.Document.FullName + ' ')
            .Append(request.Document.CodeOks + ' ')
            .Append(request.Document.ActivityField + ' ')
            .Append(request.Document.AcceptanceYear + ' ')
            .Append(request.Document.CommissionYear + ' ')
            .Append(request.Document.Author + ' ')
            .Append(request.Document.AcceptedFirstTimeOrReplaced + ' ')
            .Append(request.Document.Content + ' ')
            .Append(request.Document.KeyWords + ' ')
            .Append(request.Document.ApplicationArea + ' ')
            .Append(request.Document.Changes + ' ')
            .Append(request.Document.Amendments + ' ')
            .ToString();
        
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }

        var dictionary = Analyzer.AnalyzeText(text);
        await using var transaction = await context.Database.BeginTransactionAsync().ConfigureAwait(false);
        
        try
        {
            var dbGost = await context.Gosts.FirstOrDefaultAsync(x => x.Id == gostId).ConfigureAwait(false) ?? 
                         await gostsService.AddAsync(request.Document);
                
            context.Indexes.RemoveRange(context.Indexes.Where(x => x.GostId == gostId));
            await context.SaveChangesAsync().ConfigureAwait(false);

            foreach (var kv in dictionary)
            {
                var word = kv.Key;
                var frequency = kv.Value;

                var dbWord = await context.Words.FirstOrDefaultAsync(w => w.Content == word).ConfigureAwait(false);

                if (dbWord is null)
                {
                    dbWord = new Word { Content = word };
                    await context.Words.AddAsync(dbWord).ConfigureAwait(false);
                }

                var index = new Index
                {
                    WordId = dbWord.Id,
                    Frequency = frequency,
                    GostId = gostId
                };
                
                await context.Indexes.AddAsync(index).ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }

            dbGost.IndexedWordsCount = dictionary.Count;
            await context.SaveChangesAsync().ConfigureAwait(false);

            await transaction.CommitAsync().ConfigureAwait(false);
            return true;
        }
        catch
        {
            //Log.Error(e);
            await transaction.RollbackAsync().ConfigureAwait(false);
            return false;
        }
        finally
        {
            await transaction.DisposeAsync().ConfigureAwait(false);
        }
    }
}