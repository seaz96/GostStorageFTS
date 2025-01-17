using API.Models;
using Core.Entities;

namespace API.Services;

public interface IGostsService
{
    Task<Gost> AddAsync(Gost gost);
    Task<Gost?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task UpdateWordsIndexCount(int id, int count);
    Task UpdateDocumentStatus(UpdateStatusRequest request);
}