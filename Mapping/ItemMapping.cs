using PokemonPc.Interfaces.External;
using PokemonPc.Models;
using PokemonPc.Utils.Exceptions;

namespace PokemonPc.Mapping;

public static class ItemMapping
{
    public static Item ToItem(this ApiItem apiItem)
    {
        VersionGroupFlavorText? flavorText = apiItem.Flavor_text_entries.FirstOrDefault(e => e?.Language.Name == "en" && e.Version_group.Name == "scarlet-violet", null);
        if (flavorText == null)
        {
            throw new AtributoInexistenteException(nameof(flavorText), nameof(apiItem));
        }

        VerboseEffect? effectEntry = apiItem.Effect_entries.FirstOrDefault(e => e?.Language.Name == "en", null);
        if (effectEntry == null)
        {
            throw new AtributoInexistenteException(nameof(effectEntry), nameof(apiItem));
        }

        return new()
        {
            ExternalId = apiItem.Id,
            Name = apiItem.Name,
            Description = flavorText.Text,
            Effect = effectEntry.Effect,
            ImageUrl = apiItem.Sprites.Default,
        };
    }
}
