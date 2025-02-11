namespace PokemonPc.Models;

public class Pokedex : Model
{
    public int Number { get; set; }

    public string Name { get; set; } = null!;

    public string[] Types { get; set; } = [];

    public string Image { get; set; } = null!;
}