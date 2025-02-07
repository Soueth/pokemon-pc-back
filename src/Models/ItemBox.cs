using System;

namespace PokemonPc.Models;

public class ItemsBox : BaseModel
{
    public ICollection<Item> Item { get; set; } = null!;

    public ICollection<Trainer> Trainer { get; set; } = null!;

    public int Amount { get; set; }
}
