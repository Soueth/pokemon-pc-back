using PokemonPc.Interfaces;

namespace PokemonPc.Models;

public class MovesPokemon : BaseModel
{
    public ICollection<Moves> Move { get; set; } = null!;

    public ICollection<Pokedex> Pokemon { get; set; } = null!;

    public string LearningForm { get; set; } = null!;

    public LearningType LearningType { get; set; }
}
