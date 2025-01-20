namespace PokemonPc.Models;

public class Pokemon : BaseModel
{
    public ICollection<Pokedex> Entry { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    // TODO: implementar a foreign key para item

    // TODO: implementar a foreign key para move

    // TODO: implementar a foreign key para ability

    public int level { get; set; }

    // TODO: implementar a foreign key para trainer
}
