using System.Linq.Expressions;
using Api.Data;
using Api.Data.Repositories;
using Api.Modules.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Identity.Repositories;

public class SessionRepository(AppDbContext dbContext)
    : Repository<Session>(dbContext)
{
    public override Session? Find(Expression<Func<Session, bool>> expression)
    {
        return DbContext.Sessions
            .Include(session => session.User)
            .FirstOrDefault(expression);
    }
}
