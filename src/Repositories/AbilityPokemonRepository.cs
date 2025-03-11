using MongoDB.Driver;
using MongoDB.Entities;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;

namespace PokemonPc.Repositories;

public class AbilityPokemonRepository : Repository<AbilityPokemon>, IAbilityPokemonRepository
{
    public AbilityPokemonRepository(IMongoDatabase db)
        : base(db, APP_CONSTANTS.PROVIDERS.ABILITY_POKEMON) {}
}
