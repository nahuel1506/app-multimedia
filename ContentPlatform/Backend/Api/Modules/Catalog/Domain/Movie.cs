namespace Api.Modules.Catalog.Domain;

public class Movie : Content
{
    private Movie()
    {
        // Constructor requerido por EF Core.
    }

    public Movie(
        string title,
        string description,
        string? coverImageUrl,
        DateTime? releaseDate,
        List<string> genres)
        : base(title, description, coverImageUrl, releaseDate, genres)
    {
    }
}
