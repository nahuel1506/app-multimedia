using CsvHelper.Configuration.Attributes;

namespace Api.Data.Import.MovieLens;

public class MovieLensMovieRow
{
    [Name("movieId")]
    public int MovieId { get; set; }

    [Name("title")]
    public string Title { get; set; } = string.Empty;

    [Name("genres")]
    public string Genres { get; set; } = string.Empty;
}