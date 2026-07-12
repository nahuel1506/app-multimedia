namespace Api.Modules.Catalog.DTOs;

public record UpdateContentRequest(
    string Title,
    string Description
);