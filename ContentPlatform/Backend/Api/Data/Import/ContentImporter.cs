using System.Globalization;
using Api.Data.Import.MovieLens;
using Api.Modules.Catalog.Domain;
using Api.Modules.Catalog.Repositories;
using CsvHelper;

public class ContentImporter(ContentRepository repository)
{

    public void ImportContent(string source)
    {
        var reader = new StreamReader(source);
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var externalContents = csv
        .GetRecords<MovieLensMovieRow>()
        .Take(100)
        .ToList();

        foreach (var externalContent in externalContents)
        {
            var content = MapToContent(externalContent);
            repository.Add(content);
        }

        repository.SaveChanges();

        csv.Dispose();
        reader.Dispose();
    }


    private Movie MapToContent(MovieLensMovieRow externalContent)
    {
        var genres = externalContent.Genres
            .Split('|', StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        return new Movie(
            externalContent.Title,
            $"Genres: {externalContent.Genres}",
            null,
            null,
            genres);
    }
}
