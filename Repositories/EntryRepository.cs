using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;

namespace PokemonPc.Repositories;

public class EntryRepository : Repository<Entry>, IEntryRepository
{
    public EntryRepository(IMongoDatabase db) 
        : base(db, APP_CONSTANTS.PROVIDERS.ENTRY) {}

    public async Task<bool> HasRegister()
    {
        Entry? doc = await _collection.Find(Filter.Empty).FirstOrDefaultAsync();

        return doc != null;
    }
}
