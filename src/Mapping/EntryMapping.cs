using PokemonPc.Interfaces.External;
using PokemonPc.Models;

namespace PokemonPc.Mapping;

public static class EntryMapping
{
    public static Entry ToEntry(this ApiPokemon apiEntry)
    {
        return new()
        {
            ExternalId = apiEntry.Id,
            Name = apiEntry.Name,
            Number = apiEntry.Order,
            Types = apiEntry.Types.Select(t => t.Type.Name).ToArray(),
            ImageUrl = apiEntry.Sprites.Front_default,
            ImageUrlFemale = apiEntry.Sprites.Front_default,
            ImageUrlShiny = apiEntry.Sprites.Front_default,
            ImageUrlShinyFemale = apiEntry.Sprites.Front_default,
        };
    }
}
