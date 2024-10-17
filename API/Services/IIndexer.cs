using API.Models;

namespace API.Services;

public interface IIndexer
{
    Task<bool> TryIndexAsync(IndexRequest request);
}