using PokemonPc.Utils.Types;

namespace PokemonPc.Models;

public class MovesPokemon : Model
{
    public ICollection<Move> Move { get; set; } = null!;

    public ICollection<Pokedex> Pokemon { get; set; } = null!;

    public string LearningForm { get; set; } = null!;

    public LearningType LearningType { get; set; }
}
