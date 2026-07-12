using Api.Modules.Catalog.Domain;
using Api.Modules.Catalog.DTOs;
using Api.Modules.Catalog.Repositories;

namespace Api.Modules.Catalog.Services;

public class ContentService(ContentRepository repository)
{
    public async Task<List<ContentResponse>> GetAllAsync()
    {
        var contents = await repository.GetAllAsync();

        return contents.Select(ToResponse).ToList();
    }

    public async Task<ContentResponse?> GetByIdAsync(Guid id)
    {
        var content = await repository.GetByIdAsync(id);

        return content is null ? null : ToResponse(content);
    }

    public async Task<ContentResponse> CreateAsync(
        CreateContentRequest request)
    {
        var content = new Content(
            request.Title,
            request.Description
        );

        repository.Add(content);

        await repository.SaveChangesAsync();

        return ToResponse(content);
    }

    public async Task<ContentResponse?> UpdateAsync(
        Guid id,
        UpdateContentRequest request)
    {
        var content = await repository.GetByIdAsync(id);

        if (content is null)
            return null;

        content.Update(request.Title, request.Description);

        await repository.SaveChangesAsync();

        return ToResponse(content);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var content = await repository.GetByIdAsync(id);

        if (content is null)
            return false;

        repository.Delete(content);

        await repository.SaveChangesAsync();

        return true;
    }

    private static ContentResponse ToResponse(Content content)
    {
        return new ContentResponse(
            content.Id,
            content.Title,
            content.Description,
            content.CreatedAt
        );
    }
}