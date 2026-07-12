namespace Api.Modules.Catalog.DTOs;

public record ContentResponse(
    Guid Id,
    string Title,
    string Description,
    DateTime CreatedAt
);