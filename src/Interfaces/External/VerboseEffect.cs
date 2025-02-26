namespace PokemonPc.Interfaces.External;

public interface VerboseEffect
{
    public string Effect { get; }
    public string Short_Effect { get; }
    public NamedAPIResource Language { get; }
}