using System.Text.RegularExpressions;

namespace Api.Modules.Catalog.Domain;

public abstract class Content
{
    public Guid Id { get; protected set; }
    public string Title { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    public string? CoverImageUrl { get; protected set; }
    public DateTime? ReleaseDate { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public List<string> Genres { get; protected set; } = [];

    protected Content()
    {
        // Constructor requerido por EF Core.
        Genres = [];
    }

    protected Content(
        string title,
        string description,
        string? coverImageUrl,
        DateTime? releaseDate,
        List<string> genres)
    {
        Id = Guid.NewGuid();
        SetTitle(title);
        SetDescription(description);
        SetCoverImageUrl(coverImageUrl);
        ReleaseDate = releaseDate;
        SetGenres(genres);
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

    private void SetCoverImageUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            CoverImageUrl = null;
            return;
        }

        if (!Regex.IsMatch(url, @"^https?:\/\/[\w\-\.]+(\.[\w\-]+)+[/#?]?.*$"))
            throw new ArgumentException("La URL de la imagen de portada no es válida.");

        CoverImageUrl = url.Trim();
    }

    private void SetGenres(List<string> genres)
    {
        Genres = genres
            .Where(genre => !string.IsNullOrWhiteSpace(genre))
            .Select(genre => genre.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}
