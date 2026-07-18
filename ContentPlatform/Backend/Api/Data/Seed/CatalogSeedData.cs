using Api.Data;
using Api.Modules.Catalog.Domain;

public static class CatalogSeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Contents.Any())
            return;

        var contents = CreateContents(20);

        context.Contents.AddRange(contents);

        context.SaveChanges();
    }

    public static List<Content> CreateContents(int amount)
    {
        return Enumerable
            .Range(1, amount)
            .Select(CreateContent)
            .ToList();
    }

    public static Content CreateContent(int number)
    {
        return new Movie(
            title: $"Contenido {number}",
            description: $"Descripción de prueba para el contenido número {number}.",
            coverImageUrl: null,
            releaseDate: DateTime.UtcNow.AddYears(-number),
            genres: ["Drama"]
        );
    }
}
