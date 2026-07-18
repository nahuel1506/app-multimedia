using Api.Data;
using Api.Data.Repositories;
using Api.Modules.Catalog.Domain;

namespace Api.Modules.Catalog.Repositories;

public class ContentRepository(AppDbContext dbContext)
    : Repository<Content>(dbContext);
