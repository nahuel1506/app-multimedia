namespace Api.Modules.Identity.DTOs;

public record AuthResponse(
    Guid SessionId,
    UserResponse User
);
