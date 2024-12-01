using System.Text;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class Controller(IIndexer indexer, ISearch search, IGostsService gostsService) : ControllerBase
{
    [HttpPost("index")]
    public async Task<IActionResult> IndexAsync([FromBody] IndexRequest request)
    {
        var isSuccess = await indexer.TryIndexAsync(request).ConfigureAwait(false);

        if (!isSuccess)
            return new BadRequestResult();
        return Ok();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync([FromQuery] SearchQuery query)
    {
        return new OkObjectResult(await search.SearchAsync(query).ConfigureAwait(false));
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> SearchAsync([FromQuery] int id)
    {
        await gostsService.DeleteAsync(id).ConfigureAwait(false);
        return Ok();
    }
    
    [HttpGet("search-all")]
    public async Task<IActionResult> SearchAllAsync([FromQuery] SearchQuery query)
    {
        return new OkObjectResult(await search.SearchAllAsync(query));
    }
}