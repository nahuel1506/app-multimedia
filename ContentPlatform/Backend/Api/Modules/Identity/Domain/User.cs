using System.Text.RegularExpressions;
using Isopoh.Cryptography.Argon2;

namespace Api.Modules.Identity.Domain;

public class User
{
    public Guid Id { get; private set; }
    public string Alias { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public Role Role { get; private set; }
    public string? PhotoUrl { get; private set; }

    private User()
    {
        // Constructor requerido por EF Core.
    }

    public User(
        string alias,
        string email,
        string password,
        Role role,
        string? photoUrl = null)
    {
        Id = Guid.NewGuid();
        SetAlias(alias);
        SetEmail(email);
        SetPassword(password);
        Role = role;
        SetPhotoUrl(photoUrl);
    }

    public bool VerifyPassword(string password)
    {
        return Argon2.Verify(PasswordHash, password);
    }

    private void SetAlias(string alias)
    {
        if (string.IsNullOrWhiteSpace(alias))
            throw new ArgumentException("El alias es obligatorio.");

        if (alias.Length < 3 || alias.Length > 30)
            throw new ArgumentException("El alias debe tener entre 3 y 30 caracteres.");

        if (!Regex.IsMatch(alias, @"^[a-zA-Z0-9_ ]+$"))
            throw new ArgumentException(
                "El alias solo puede contener letras, números, guiones bajos y espacios.");

        Alias = alias.Trim();
    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El email es obligatorio.");

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("El email no tiene un formato válido.");

        Email = email.Trim().ToLowerInvariant();
    }

    private void SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("La contraseña es obligatoria.");

        if (password.Length < 6)
            throw new ArgumentException(
                "La contraseña debe tener al menos 6 caracteres.");

        PasswordHash = Argon2.Hash(password);
    }

    private void SetPhotoUrl(string? photoUrl)
    {
        if (string.IsNullOrWhiteSpace(photoUrl))
        {
            PhotoUrl = null;
            return;
        }

        if (!Uri.TryCreate(photoUrl, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException("La URL de la foto no es válida.");
        }

        PhotoUrl = photoUrl.Trim();
    }
}
