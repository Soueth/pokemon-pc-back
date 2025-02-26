namespace PokemonPc.Interfaces.External;

public interface Effect {
    public string Effect { get; set; }
    public NamedAPIResource Language { get; set; }
}