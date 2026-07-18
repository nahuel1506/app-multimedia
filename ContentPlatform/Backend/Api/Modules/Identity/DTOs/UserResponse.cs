namespace Api.Modules.Identity.DTOs;

public record UserResponse(
    Guid Id,
    string Alias,
    string Email,
    string Role,
    string? PhotoUrl
);
