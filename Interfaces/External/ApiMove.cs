namespace PokemonPc.Interfaces.External;

public interface ApiMove
{
    public int Id { get; }
    public string Name { get; }
    public int Accuracy { get; }
    public int Effect_chance { get; }
    public int Pp { get; }
    public int Priority { get; }
    public int Power { get; }
    public ContestCombos Contest_combos { get; }
    public NamedAPIResource Contest_type { get; }
    public object Contest_effect { get; }
    public NamedAPIResource Damage_class { get; }
    public VerboseEffect[] Effect_entries { get; }
    public AbilityEffectChange[] Effect_changes { get; }
    public NamedAPIResource[] Learned_by_pokemon { get; }
    public VersionGroupFlavorText[] Flavor_text_entries { get; }
    public object Generation { get; }
    public object[] Machines { get; }
    public object Meta { get; }
    public Name[] Names { get; }
    public object[] Past_values { get; }
    public MoveStatChange[] Stat_change { get; }
    public object Super_contest_effect { get; }
    public NamedAPIResource Target { get; }
    public NamedAPIResource Type { get; }
}

public interface ContestCombos
{
    public ContestCombosDetails Normal { get; }
    public ContestCombosDetails Super { get; }
}

public interface ContestCombosDetails
{
    public NamedAPIResource[] Use_before { get; }
    public NamedAPIResource[] Use_after { get; }
}

public interface MoveStatChange
{
    public int Change { get; }
    public NamedAPIResource Move { get; }
}