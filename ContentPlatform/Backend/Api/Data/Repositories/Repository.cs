using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories;

public class Repository<T>(AppDbContext dbContext) where T : class
{
    protected AppDbContext DbContext { get; } = dbContext;
    protected DbSet<T> Entities { get; } = dbContext.Set<T>();

    public virtual T? Find(Expression<Func<T, bool>> expression)
    {
        return Entities.FirstOrDefault(expression);
    }

    public List<T> FindAll(Expression<Func<T, bool>>? expression = null)
    {
        return expression is null
            ? Entities.ToList()
            : Entities.Where(expression).ToList();
    }

    public bool Exists(Expression<Func<T, bool>> expression)
    {
        return Entities.Any(expression);
    }

    public void Add(T entity)
    {
        Entities.Add(entity);
    }

    public void Delete(T entity)
    {
        Entities.Remove(entity);
    }

    public void SaveChanges()
    {
        DbContext.SaveChanges();
    }
}
