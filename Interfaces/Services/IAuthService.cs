namespace PokemonPc.Interfaces.Utils;

public interface IAuthService
{
    string GenerateToken(string uuid);

    CookieOptions GetAuthCookieOptions();
}
