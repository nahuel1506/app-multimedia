using Api.Data;
using Api.Data.Repositories;
using Api.Modules.Identity.Domain;

namespace Api.Modules.Identity.Repositories;

public class UserRepository(AppDbContext dbContext)
    : Repository<User>(dbContext);
