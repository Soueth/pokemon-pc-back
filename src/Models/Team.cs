using System;
using PokemonPc.Models;

namespace PokemonPc.Models;

public class Teams : Model
{
    public string Name { get; set; } = null!;

    public ICollection<Trainer> Trainer { get; set; } = null!;

    public ICollection<Pokemon>[] Pokemon { get; set; } = [];
}
