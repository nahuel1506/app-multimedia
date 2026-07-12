using Api.Modules.Catalog.DTOs;
using Api.Modules.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Catalog.Controllers;

[ApiController]
[Route("api/contents")]
public class ContentsController(ContentService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ContentResponse>>> GetAll()
    {
        var contents = await service.GetAllAsync();

        return Ok(contents);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ContentResponse>> GetById(Guid id)
    {
        var content = await service.GetByIdAsync(id);

        if (content is null)
            return NotFound();

        return Ok(content);
    }

    [HttpPost]
    public async Task<ActionResult<ContentResponse>> Create(
        [FromBody] CreateContentRequest request)
    {
        var content = await service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = content.Id },
            content
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ContentResponse>> Update(
        Guid id,
        [FromBody] UpdateContentRequest request)
    {
        var content = await service.UpdateAsync(id, request);

        if (content is null)
            return NotFound();

        return Ok(content);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}