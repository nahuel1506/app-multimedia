namespace Api.Modules.Identity.DTOs;

public record LoginRequest(
    string Email,
    string Password
);
