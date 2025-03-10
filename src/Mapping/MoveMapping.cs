using PokemonPc.Interfaces.External;
using PokemonPc.Models;
using PokemonPc.Utils.Exceptions;

namespace PokemonPc.Mapping;

public static class MoveMapping
{
    public static Move ToMove(this ApiMove apiMove)
    {
        VersionGroupFlavorText? flavorText = apiMove.Flavor_text_entries.FirstOrDefault(e => e?.Language.Name == "en" && e.Version_group.Name == "scarlet-violet", null);
        VerboseEffect? effectEntry = apiMove.Effect_entries.FirstOrDefault(e => e?.Language.Name == "en", null);

        if (flavorText == null)
        {
            throw new AtributoInexistenteException(nameof(flavorText), nameof(apiMove));
        }

        if (effectEntry == null)
        {
            throw new AtributoInexistenteException(nameof(effectEntry), nameof(apiMove));
        }


        return new()
        {
            ExternalId = apiMove.Id,
            Description = flavorText.Text,
            Name = apiMove.Name,
            Effect = effectEntry.Effect,
        };
    }
}
