using Presentation.WebApp.Models;
using System.Security.Cryptography;

namespace Presentation.WebApp.Services;

public interface IPasswordHasher
{
    PasswordHashResult HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword, string hashedSalt);
}

public class Pbkdf2PasswordHasher : IPasswordHasher
{
    private const string _staticSalt = "ODUxMTg4YTUtOWY0NS00ZWNmLWE1OTMtNzlkMDE0MzIwYjlh";
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Itertions = 100000;


    public PasswordHashResult HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var pwd = $"{password}.{_staticSalt}";

        var hash = Rfc2898DeriveBytes.Pbkdf2
        (
            pwd,
            salt,
            Itertions,
            HashAlgorithmName.SHA256,
            KeySize
        );

        return new PasswordHashResult
        (
            Convert.ToBase64String(hash),
            Convert.ToBase64String(salt)
        );
    }

    public bool VerifyPassword(string password, string hashedPassword, string hashedSalt)
    {
        var pwd = $"{password}.{_staticSalt}";
        var salt = Convert.FromBase64String(hashedSalt);
        var expectedHash = Convert.FromBase64String(hashedPassword);

        var hash = Rfc2898DeriveBytes.Pbkdf2
        (
            pwd,
            salt,
            Itertions,
            HashAlgorithmName.SHA256,
            expectedHash.Length
        );

        return CryptographicOperations.FixedTimeEquals(hash, expectedHash);

    }
}
