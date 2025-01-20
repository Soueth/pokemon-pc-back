using System;
using PokemonPc.Models;

namespace PokemonPc.Models;

public class Teams : BaseModel
{
    public string Name { get; set; } = null!;

    public ICollection<Trainers> Trainer { get; set; } = null!;

    public ICollection<Pokemon>[] Pokemon { get; set; } = [];
}
