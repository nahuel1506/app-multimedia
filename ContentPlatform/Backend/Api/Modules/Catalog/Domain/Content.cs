namespace Api.Modules.Catalog.Domain;

public class Content
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private Content()
    {
        // Constructor requerido por EF Core.
    }

    public Content(string title, string description)
    {
        Id = Guid.NewGuid();
        SetTitle(title);
        SetDescription(description);
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string title, string description)
    {
        SetTitle(title);
        SetDescription(description);
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("El título es obligatorio.");

        if (title.Length > 150)
            throw new ArgumentException("El título no puede superar 150 caracteres.");

        Title = title.Trim();
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("La descripción es obligatoria.");

        Description = description.Trim();
    }
}