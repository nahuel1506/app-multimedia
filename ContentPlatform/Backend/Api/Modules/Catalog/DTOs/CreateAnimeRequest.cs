namespace Api.Modules.Catalog.DTOs;

public record CreateAnimeRequest(
    string Title,
    string Description,
    string? CoverImageUrl,
    DateTime? ReleaseDate,
    List<string> Genres
);
