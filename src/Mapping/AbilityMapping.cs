using PokemonPc.Interfaces.External;
using PokemonPc.Models;
using PokemonPc.Utils.Exceptions;

namespace PokemonPc.Mapping;

public static class AbilityMapping
{
    public static Ability ToAbility(this ApiAbility apiAbility)
    {
        AbilityFlavorText? flavorEffect = apiAbility.Flavor_text_entries.FirstOrDefault(e => e?.Language.Name == "en" && e.Version_group.Name == "scarlet-violet", null);
        VerboseEffect? effectEntry = apiAbility.Effect_entries.FirstOrDefault(e => e?.Language.Name == "en", null);

        if (flavorEffect == null)
        {
            throw new AtributoInexistenteException(nameof(flavorEffect), nameof(apiAbility));
        }

        if (effectEntry == null)
        {
            throw new AtributoInexistenteException(nameof(effectEntry), nameof(apiAbility));
        }

        return new()
        {
            ExtenalId = apiAbility.Id,
            Description = flavorEffect.Flavor_text,
            Name = apiAbility.Name,
            Effect = effectEntry.Effect,
        };
    }
}
