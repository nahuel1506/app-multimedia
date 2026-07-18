namespace Api.Modules.Identity.DTOs;

public record SignUpRequest(
    string Alias,
    string Email,
    string Password,
    string PasswordConfirmation,
    string? PhotoUrl
);
