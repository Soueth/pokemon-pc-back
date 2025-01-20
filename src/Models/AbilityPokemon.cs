using System;

namespace PokemonPc.Models;

public class AbilityPokemon : BaseModel
{
    public ICollection<Abilities> Ability { get; set; } = null!;

    public ICollection<Pokemon> Pokemon { get; set; } = null!;
}
