namespace Api.Modules.Catalog.DTOs;

public record CreateMovieRequest(
    string Title,
    string Description,
    string? CoverImageUrl,
    DateTime? ReleaseDate,
    List<string> Genres
);
