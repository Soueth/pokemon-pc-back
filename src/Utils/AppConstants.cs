using PokemonPc.Models;

namespace PokemonPc.Constants;

public readonly struct PROVIDERS
{
    public string USER { get; }
    public string TRAINER { get; }

    public PROVIDERS()
    {
        USER = "users";
        TRAINER = "trainers";
    }
}
public static class AppConstants
{
    public static readonly PROVIDERS PROVIDERS = new PROVIDERS();
}