using System.Text;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class Controller(IGostsService gostsService, IIndexer indexer, ISearch search) : ControllerBase
{
    [HttpPost("index")]
    public async Task<IActionResult> IndexAsync([FromBody] IndexRequest request)
    {
        var sb = new StringBuilder(request.Text)
            .Append(request.Document.Designation)
            .Append(request.Document.FullName)
            .Append(request.Document.CodeOKS)
            .Append(request.Document.ActivityField)
            .Append(request.Document.AcceptanceYear)
            .Append(request.Document.CommissionYear)
            .Append(request.Document.Author)
            .Append(request.Document.AcceptedFirstTimeOrReplaced)
            .Append(request.Document.Content)
            .Append(request.Document.KeyWords)
            .Append(request.Document.ApplicationArea)
            .Append(request.Document.AdoptionLevel)
            .Append(request.Document.Changes)
            .Append(request.Document.Amendments)
            .Append(request.Document.Harmonization);

        await gostsService.AddAsync(request.Document);
        var isSuccess = await indexer.TryIndexAsync(request.Document.Id, sb.ToString());

        if (!isSuccess)
            return new BadRequestResult();
        return Ok();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync([FromQuery] SearchQuery query)
    {
        return new OkObjectResult(await search.SearchAsync(query));
    }
}