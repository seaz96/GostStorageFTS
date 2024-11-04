using System.Text;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class Controller(IIndexer indexer, ISearch search) : ControllerBase
{
    [HttpPost("index")]
    public async Task<IActionResult> IndexAsync([FromBody] IndexRequest request)
    {
        var isSuccess = await indexer.TryIndexAsync(request);

        if (!isSuccess)
            return new BadRequestResult();
        return Ok();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync([FromQuery] SearchQuery query)
    {
        return new OkObjectResult(await search.SearchAsync(query));
    }
    
    [HttpGet("search-all")]
    public async Task<IActionResult> SearchAllAsync([FromQuery] SearchQuery query)
    {
        return new OkObjectResult(await search.SearchAll(query));
    }
}