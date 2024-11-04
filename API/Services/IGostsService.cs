using Core.Entities;

namespace API.Services;

public interface IGostsService
{
    Task<Gost> AddAsync(Gost gost);
    Task<Gost?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(long id);
}