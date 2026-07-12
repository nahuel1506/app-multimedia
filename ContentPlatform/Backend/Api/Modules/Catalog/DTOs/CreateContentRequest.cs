namespace Api.Modules.Catalog.DTOs;

public record CreateContentRequest(
    string Title,
    string Description
);