namespace PokemonPc.Models;

public class Pokedex : BaseModel
{
    public int Number { get; set; }

    public string Name { get; set; } = null!;

    public string[] Types { get; set; } = [];
}