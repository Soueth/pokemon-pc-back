namespace PokemonPc.Utils.Functions;

public static class Encrypter
{
    public static string HashPassword(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public static bool VerifyPassword(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);

    public static CookieOptions GenerateCookie(bool httpsOnly, int lifetime)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = httpsOnly,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(lifetime)
        };
    }
}