namespace PokemonPc.Interfaces.External;

public interface ApiItem
{
    public int Id { get; }
    public string Name { get; }
    public int Cost { get; }
    public int Fling_power { get; }
    public NamedAPIResource Fling_effect { get; }
    public NamedAPIResource Attributes { get; }
    public NamedAPIResource Category { get; }
    public VerboseEffect[] Effect_entries { get; }
    public VersionGroupFlavorText[] Flavor_effect_entries { get; }
    public GenerationGameIndex[] Game_indices { get; }
    public Name[] Names { get; }
    public ItemSprites Sprites { get; }
    public object[] Held_by_pokemon { get; }
    public object Baby_trigger_for { get; }
    public object[] Machines { get; }
}
