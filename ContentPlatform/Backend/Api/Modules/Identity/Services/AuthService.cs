using Api.Modules.Identity.Domain;
using Api.Modules.Identity.DTOs;
using Api.Modules.Identity.Repositories;

namespace Api.Modules.Identity.Services;

public class AuthService(
    UserRepository userRepository,
    SessionRepository sessionRepository)
{
    public AuthResponse SignUp(SignUpRequest request)
    {
        if (request.Password != request.PasswordConfirmation)
            throw new ArgumentException("Las contraseñas no coinciden.");

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        if (userRepository.Exists(user => user.Email == normalizedEmail))
            throw new InvalidOperationException(
                "Ya existe un usuario con ese email.");

        var user = new User(
            request.Alias,
            request.Email,
            request.Password,
            Role.User,
            request.PhotoUrl
        );

        var session = new Session(user);

        userRepository.Add(user);
        sessionRepository.Add(session);
        sessionRepository.SaveChanges();

        return ToAuthResponse(session, user);
    }

    public AuthResponse Login(LoginRequest request)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = userRepository.Find(user => user.Email == normalizedEmail);

        if (user is null || !user.VerifyPassword(request.Password))
            throw new InvalidOperationException("Credenciales inválidas.");

        if (sessionRepository.Exists(session => session.UserId == user.Id))
            throw new InvalidOperationException("El usuario ya inició sesión.");

        var session = new Session(user);

        sessionRepository.Add(session);
        sessionRepository.SaveChanges();

        return ToAuthResponse(session, user);
    }

    // public AuthResponse LoginExternal(ExternalLoginRequest externalLogin)
    // {
    //     var handler = externalLoginHandler.GetHandler(externalLogin.Provider);
    //     var externalUserData = handler.GetUserData(externalLogin.Token);

    //     if (externalUserData == null)
    //     {
    //         throw new ArgumentException("Credenciales invalidas");
    //     }
        
    // }

    public bool Logout(Guid sessionId)
    {
        var session = sessionRepository.Find(session => session.Id == sessionId);

        if (session is null)
            return false;

        sessionRepository.Delete(session);
        sessionRepository.SaveChanges();

        return true;
    }

    private static AuthResponse ToAuthResponse(Session session, User user)
    {
        return new AuthResponse(
            session.Id,
            UserService.ToResponse(user)
        );
    }
}
