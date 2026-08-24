using System.Security.Cryptography;
using SistemaILP.Ruteo.Application.Interfaces;

namespace SistemaILP.Ruteo.Infrastructure.Security;

/// <summary>
/// PBKDF2 (Rfc2898) password hashing with a random per-user salt.
/// No external dependency required, safe for local/offline auth.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100_000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    public (string Hash, string Salt) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var key = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, KeySize);

        return (Convert.ToBase64String(key), Convert.ToBase64String(salt));
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var expectedKey = Convert.FromBase64String(hash);

        var actualKey = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, Iterations, Algorithm, KeySize);

        return CryptographicOperations.FixedTimeEquals(actualKey, expectedKey);
    }
}
