using Api.Modules.Catalog.Domain;
using Api.Modules.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Content> Contents => Set<Content>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Anime> Animes => Set<Anime>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Session> Sessions => Set<Session>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Content
        modelBuilder.Entity<Content>()
            .HasDiscriminator<string>("ContentType")
            .HasValue<Movie>("Movie")
            .HasValue<Anime>("Anime");

        //User
        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        //Session
        modelBuilder.Entity<Session>()
            .HasIndex(session => session.UserId)
            .IsUnique();

        modelBuilder.Entity<Session>()
            .HasOne(session => session.User)
            .WithMany()
            .HasForeignKey(session => session.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
