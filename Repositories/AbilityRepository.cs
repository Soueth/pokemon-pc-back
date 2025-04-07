using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;

namespace PokemonPc.Repositories;

public class AbilityRepository : Repository<Ability>, IAbilityRepository
{
    public AbilityRepository(IMongoDatabase db) 
        : base(db, APP_CONSTANTS.PROVIDERS.ABILITY) {}

    public async Task<bool> HasRegister()
    {
        Ability? doc = await _collection.Find(Filter.Empty).FirstOrDefaultAsync();
        return doc != null;
    }
}
