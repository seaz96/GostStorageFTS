using API.Data;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class GostsService(DataContext context) : IGostsService
{
    public async Task AddAsync(Gost gost)
    {
        await context.Gosts.AddAsync(gost);
        await context.SaveChangesAsync();
    }

    public Task<Gost?> GetByIdAsync(int id)
    {
        return context.Gosts.FirstOrDefaultAsync(gost => gost.Id == id);
    }
}