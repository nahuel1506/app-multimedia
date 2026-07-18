using Api.Modules.Identity.Domain;
using Api.Modules.Identity.DTOs;
using Api.Modules.Identity.Repositories;

namespace Api.Modules.Identity.Services;

public class UserService(UserRepository userRepository)
{
    public UserResponse? GetById(Guid id)
    {
        var user = userRepository.Find(user => user.Id == id);

        return user is null ? null : ToResponse(user);
    }

    internal static UserResponse ToResponse(User user)
    {
        return new UserResponse(
            user.Id,
            user.Alias,
            user.Email,
            user.Role.ToString(),
            user.PhotoUrl
        );
    }
}
