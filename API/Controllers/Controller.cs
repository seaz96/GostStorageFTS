using API.Models;
using API.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class Controller(IIndexer indexer, ISearch search, IGostsService gostsService) : ControllerBase
{
    [HttpPost("index")]
    public async Task<IActionResult> IndexAsync([FromBody] IndexRequest request)
    {
        return await indexer.TryIndexAsync(request).ConfigureAwait(false) 
            ? Ok()
            : new BadRequestResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync([FromQuery] SearchQuery query)
    {
        return new OkObjectResult(await search.SearchAsync(query).ConfigureAwait(false));
    }
    
    [HttpGet("count")]
    public async Task<IActionResult> CountAsync([FromQuery] SearchQuery query)
    {
        return new OkObjectResult(await search.CountAsync(query).ConfigureAwait(false));
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> SearchAsync([FromQuery] int id)
    {
        await gostsService.DeleteAsync(id).ConfigureAwait(false);
        return Ok();
    }

    [HttpPost("update-status")]
    public async Task<IActionResult> UpdateStatusAsync([FromBody] UpdateStatusRequest request)
    {
        await gostsService.UpdateDocumentStatus(request).ConfigureAwait(false);
        return Ok();
    }
}