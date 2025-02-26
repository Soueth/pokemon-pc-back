using PokemonPc.Interfaces.External;
using PokemonPc.Models;

namespace PokemonPc.Mapping;

public static class ItemMapping
{
    public static Item ToItem(this ApiItem item)
    {
        return new()
        {
            Name = item.Name,
            ImageUrl = item.Sprites.Default,
            Description = item
        };
    }
}
