using API.Data;
using API.Models;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class GostsService(DataContext context) : IGostsService
{
    public async Task<Gost> AddAsync(Gost gost)
    {
        var dbGostEntry = await context.Gosts.AddAsync(gost).ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);
        return dbGostEntry.Entity;
    }

    public Task<Gost?> GetByIdAsync(int id)
    {
        return context.Gosts.FirstOrDefaultAsync(gost => gost.Id == id);
    }

    public async Task DeleteAsync(int id)
    {
        var dbGostEntry = await context.Gosts.FirstOrDefaultAsync(gost => gost.Id == id).ConfigureAwait(false);
        
        if (dbGostEntry is null)
            return;

        var indexes = context.Indexes.Where(i => i.GostId == id);
        
        context.Gosts.Remove(dbGostEntry);
        context.Indexes.RemoveRange(indexes);
        
        await context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task UpdateWordsIndexCount(int id, int count)
    {
        var dbGostEntry = await context.Gosts.FirstOrDefaultAsync(gost => gost.Id == id).ConfigureAwait(false);
        
        if (dbGostEntry is null)
            return;
        
        dbGostEntry.IndexedWordsCount = count;
        context.Gosts.Update(dbGostEntry);
        await context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task UpdateDocumentStatus(UpdateStatusRequest request)
    {
        var dbGostEntry = await context.Gosts
            .FirstOrDefaultAsync(document => document.Id == request.Id)
            .ConfigureAwait(false);
        
        if (dbGostEntry is null)
            return;
        
        dbGostEntry.Status = request.Status;
        context.Gosts.Update(dbGostEntry);
        await context.SaveChangesAsync().ConfigureAwait(false);
    }
}