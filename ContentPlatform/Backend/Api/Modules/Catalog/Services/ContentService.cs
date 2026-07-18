using Api.Modules.Catalog.Domain;
using Api.Modules.Catalog.DTOs;
using Api.Modules.Catalog.Repositories;
using Api.Modules.Identity.Domain;
using Api.Modules.Identity.Services;

namespace Api.Modules.Catalog.Services;

public class ContentService(
    ContentRepository repository,
    AuthorizationService authorizationService,
    AuthenticationService authenticationService)
{
    public List<ContentResponse> GetAll()
    {
        var contents = repository.FindAll();

        return contents.Select(ToResponse).ToList();
    }

    public ContentResponse? GetById(Guid id)
    {
        var content = repository.Find(content => content.Id == id);

        return content is null ? null : ToResponse(content);
    }

    public ContentResponse CreateMovie(Guid sessionId, CreateMovieRequest request)
    {
        var movie = new Movie(
            request.Title,
            request.Description,
            request.CoverImageUrl,
            request.ReleaseDate,
            request.Genres
        );

        return SaveContent(sessionId, movie);
    }

    public ContentResponse CreateAnime(Guid sessionId, CreateAnimeRequest request)
    {
        var anime = new Anime(
            request.Title,
            request.Description,
            request.CoverImageUrl,
            request.ReleaseDate,
            request.Genres
        );

        return SaveContent(sessionId, anime);
    }

    public ContentResponse? Update(Guid id, UpdateContentRequest request)
    {
        var content = repository.Find(content => content.Id == id);

        if (content is null)
            return null;

        content.Update(request.Title, request.Description);
        repository.SaveChanges();

        return ToResponse(content);
    }

    public bool Delete(Guid id)
    {
        var content = repository.Find(content => content.Id == id);

        if (content is null)
            return false;

        repository.Delete(content);
        repository.SaveChanges();

        return true;
    }

    private ContentResponse SaveContent(Guid sessionId, Content content)
    {
        var user = authenticationService.GetUserFromSession(sessionId);
        if (user == null)
        {
            throw new ArgumentException("El usuario no está autenticado.");
        }

        var isAllowed = authorizationService.UserHasRole(user, Role.Admin);
        var contentType = content.GetType().Name;
        var alreadyExists = repository.Exists(c => c.Title == content.Title && c.GetType().Name == contentType);
        if (alreadyExists)
        {
            throw new ArgumentException($"Ya existe un {content} con este título");
        }
        repository.Add(content);
        repository.SaveChanges();

        return ToResponse(content);
    }

    private static ContentResponse ToResponse(Content content)
    {
        return new ContentResponse(
            content.Id,
            content.GetType().Name,
            content.Title,
            content.Description,
            content.CoverImageUrl,
            content.ReleaseDate,
            content.Genres,
            content.CreatedAt
        );
    }
}
