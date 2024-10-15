namespace API.Services;

public interface IIndexer
{
    Task<bool> TryIndexAsync(int gostId, string text);
}