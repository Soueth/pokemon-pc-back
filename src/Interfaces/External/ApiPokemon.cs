namespace PokemonPc.Interfaces.External;

public interface VersionGameIndex
{
    public int Game_index { get; }
    public NamedAPIResource Version { get; }
}

public interface PokemonStat
{
    public NamedAPIResource Stat { get; }
    public int Effort { get; }
    public int Base_stat { get; }
}

public interface PokemonType
{
    public int Slot { get; }
    public NamedAPIResource Type { get; }
}

public interface ApiPokemonMove
{
    public NamedAPIResource Move { get; }
    public VersionGroupDetails[] Version_group_details { get; }
}

public interface VersionGroupDetails
{
    public int? Level_learned_at { get; }
    public NamedAPIResource Move_learn_method { get; }
    public NamedAPIResource Version_group { get; }
}

public interface PokemonSprites
{
    public string Front_default { get; }
    public string Front_shiny { get; }
    public string Front_female { get; }
    public string Front_shiny_default { get; }
    public string Back_default { get; }
    public string Back_shiny { get; }
    public string Back_female { get; }
    public string Back_shiny_default { get; }
}

public interface ApiPokemon
{
    public int Id { get; }
    public string Name { get; }
    public int Base_experience { get; }
    public int Height { get; }
    public bool Is_default { get; }
    public int Order { get; }
    public int Weight { get; }
    public ApiAbility[] Abilities { get; }
    public NamedAPIResource Forms { get; }
    public VersionGameIndex[] Game_indices { get; }
    public object[] held_items { get; }
    public string Location_area_encounters { get; }
    public ApiPokemonMove[] Moves { get; }
    public object[] Past_types { get; }
    public PokemonSprites Sprites { get; }
    public NamedAPIResource Cries { get; }
    public NamedAPIResource Species { get; }
    public PokemonStat[] Stats { get; }
    public PokemonType[] Types { get; }
}
