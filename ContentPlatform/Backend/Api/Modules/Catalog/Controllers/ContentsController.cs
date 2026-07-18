using Api.Modules.Catalog.DTOs;
using Api.Modules.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Catalog.Controllers;

[ApiController]
[Route("api/contents")]
public class ContentsController(ContentService service) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<ContentResponse>> GetAll()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<ContentResponse> GetById(Guid id)
    {
        var content = service.GetById(id);

        if (content is null)
            return NotFound();

        return Ok(content);
    }

    [HttpPost("movies")]
    public ActionResult<ContentResponse> CreateMovie(
        [FromBody] CreateMovieRequest request)
    {
        var content = service.CreateMovie(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = content.Id },
            content
        );
    }

    [HttpPost("animes")]
    public ActionResult<ContentResponse> CreateAnime(
        [FromBody] CreateAnimeRequest request)
    {
        var content = service.CreateAnime(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = content.Id },
            content
        );
    }

    [HttpPut("{id:guid}")]
    public ActionResult<ContentResponse> Update(
        Guid id,
        [FromBody] UpdateContentRequest request)
    {
        var content = service.Update(id, request);

        if (content is null)
            return NotFound();

        return Ok(content);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var deleted = service.Delete(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
