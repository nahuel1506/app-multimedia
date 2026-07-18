using Api.Modules.Identity.Domain;

namespace Api.Modules.Identity.Services;

public class AuthorizationService
{
    public bool UserHasRole(User user, Role role)
    {
        return user.Role == role;
    }
}
