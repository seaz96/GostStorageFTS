using API.Models;

namespace API.Services;

public interface ISearch
{
    Task<List<SearchEntity>> SearchAsync(SearchQuery query);
}