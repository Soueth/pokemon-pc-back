namespace PokemonPc.Interfaces.External;

public interface VersionGroupFlavorText
{
    public string Text { get; }
    public NamedAPIResource Language { get; }
    public NamedAPIResource Version_group { get; }
}
