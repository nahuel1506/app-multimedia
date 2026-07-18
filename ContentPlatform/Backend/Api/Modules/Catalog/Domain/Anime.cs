namespace Api.Modules.Catalog.Domain;

public class Anime : Content
{
    private Anime()
    {
        // Constructor requerido por EF Core.
    }

    public Anime(
        string title,
        string description,
        string? coverImageUrl,
        DateTime? releaseDate,
        List<string> genres)
        : base(title, description, coverImageUrl, releaseDate, genres)
    {
    }
}
