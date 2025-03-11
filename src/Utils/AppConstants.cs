using PokemonPc.Models;

namespace PokemonPc.Constants;

public readonly struct PROVIDERS
{
    public string USER { get; }
    public string TRAINER { get; }
    public string ENTRY { get; }
    public string ITEM { get; }
    public string MOVE { get; }
    public string ABILITY { get; }
    public string POKEMON { get; }
    public string MOVE_POKEMON { get; }
    public string ABILITY_POKEMON { get; }

    public PROVIDERS()
    {
        USER = "users";
        TRAINER = "trainers";
        ENTRY = "Entry";
        ITEM = "items";
        MOVE = "moves";
        ABILITY = "abilities";
        POKEMON = "pokemon";
        MOVE_POKEMON = "move-pokemon";
        ABILITY_POKEMON = "ability-pokemon";
    }
}

public readonly struct ERROR_MESSAGES
{
    public string MUST_BE_STRING { get; }
    public string UNEXPECTED_ERROR { get; }

    public ERROR_MESSAGES()
    {
        MUST_BE_STRING = "Value must be a string";
        UNEXPECTED_ERROR = "Ocorreu um erro inesperado";
    }
}

public static class APP_CONSTANTS
{
    public static readonly PROVIDERS PROVIDERS = new PROVIDERS();
    public static readonly Guid USERS_GUID = Guid.NewGuid();
    public static readonly ERROR_MESSAGES ERROR_MESSAGES = new ERROR_MESSAGES();
}