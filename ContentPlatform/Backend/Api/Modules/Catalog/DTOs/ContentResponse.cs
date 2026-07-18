namespace Api.Modules.Catalog.DTOs;

public record ContentResponse(
    Guid Id,
    string Type,
    string Title,
    string Description,
    string? CoverImageUrl,
    DateTime? ReleaseDate,
    List<string> Genres,
    DateTime CreatedAt
);
