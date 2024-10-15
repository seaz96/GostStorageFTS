using Core.Entities;

namespace API.Services;

public interface IGostsService
{
    Task AddAsync(Gost gost);
    Task<Gost?> GetByIdAsync(int id);
}