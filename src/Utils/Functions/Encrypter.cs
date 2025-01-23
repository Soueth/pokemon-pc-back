using System.Security.Cryptography;

namespace PokemonPc.Utils.Functions;

public static class Encrypter
{
    public static string HashPassword(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public static bool VerifyPassword(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);
}