namespace PokemonPc.Models;

public class AbilityPokemon : Model
{
    public ICollection<Ability> Ability { get; set; } = null!;

    public ICollection<Pokemon> Pokemon { get; set; } = null!;
}
