using Api.Modules.Identity.Domain;
using Api.Modules.Identity.Repositories;

namespace Api.Modules.Identity.Services;

public class AuthenticationService(SessionRepository sessionRepository)
{
    public User? GetUserFromSession(Guid sessionId)
    {
        return sessionRepository.Find(session => session.Id == sessionId)?.User;
    }
}
