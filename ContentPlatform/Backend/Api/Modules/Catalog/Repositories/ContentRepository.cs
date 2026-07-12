using Api.Data;
using Api.Modules.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Catalog.Repositories;

public class ContentRepository(AppDbContext dbContext)
{
    public Task<List<Content>> GetAllAsync()
    {
        return dbContext.Contents.ToListAsync();
    }

    public Task<Content?> GetByIdAsync(Guid id)
    {
        return dbContext.Contents
            .FirstOrDefaultAsync(content => content.Id == id);
    }

    public void Add(Content content)
    {
        dbContext.Contents.Add(content);
    }

    public void Delete(Content content)
    {
        dbContext.Contents.Remove(content);
    }

    public Task SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }
}