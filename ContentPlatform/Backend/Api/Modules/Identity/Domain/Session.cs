namespace Api.Modules.Identity.Domain;

public class Session
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    private Session()
    {
        // Constructor requerido por EF Core.
    }

    public Session(User user)
    {
        Id = Guid.NewGuid();
        User = user ?? throw new ArgumentNullException(nameof(user));
        UserId = user.Id;
    }
}
