namespace StoreBLL.Helpers;

using StoreDAL.Data.InitDataFactory;
using System.Security.Cryptography;

/// <summary>
/// Simple password helper using only built-in .NET cryptography.
/// </summary>
public static class PasswordHelper
{
    /// <summary>
    /// Hashes a password using PBKDF2 with a random salt.
    /// </summary>
    /// <param name="password">Plain text password to hash.</param>
    /// <returns>Base64 encoded salt+hash.</returns>
    public static string HashPassword(string password)
    {
        // Generate random salt
        return TestDataFactory.HashPassword(password);
    }

    /// <summary>
    /// Verifies a password against its hash.
    /// </summary>
    /// <param name="password">Plain text password to verify.</param>
    /// <param name="storedHash">Base64 encoded salt+hash.</param>
    /// <returns>True if password matches.</returns>
    public static bool VerifyPassword(string password, string storedHash)
    {
        // Decode the hash
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        // Extract salt (first 16 bytes)
        byte[] salt = hashBytes[..16];

        // Hash the password with the same salt
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);

        // Compare hashes using constant-time comparison
        return CryptographicOperations.FixedTimeEquals(hashBytes[16..], hash);
    }
}