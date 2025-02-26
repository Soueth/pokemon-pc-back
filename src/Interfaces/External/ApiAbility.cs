namespace PokemonPc.Interfaces.External;

public interface ApiGenerationAbility
{
    public string Name { get; }
    public string url { get; }
}

public interface AbilityEffectChange
{
    public Effect Effect_entries { get; }
    public NamedAPIResource Version_group { get; }
}

public interface AbilityFlavorText
{
    public string Flavor_text { get; }
    public NamedAPIResource Language { get; }
    public NamedAPIResource Version_group { get; }

}

public interface AbilityPokemon
{
    public bool Is_hidden { get; }
    public int Slot { get; }
    public NamedAPIResource Pokemon { get; }
}

public interface ApiAbility
{
    public int Id { get; }
    public string Name { get; }
    public bool Is_main_series { get; }
    public object Generation { get; }
    public string[] Names { get; }
    public VerboseEffect[] Effect_entries { get; }
    public AbilityEffectChange[] Effect_changes { get; }
    public AbilityFlavorText[] Flavor_text_entries { get; }
    public AbilityPokemon[] Pokemon { get; }
}
