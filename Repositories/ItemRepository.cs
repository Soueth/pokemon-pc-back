using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;

namespace PokemonPc.Repositories;

public class ItemRepository : Repository<Item>, IItemRepository
{
    public ItemRepository(IMongoDatabase db) : base(db, APP_CONSTANTS.PROVIDERS.ITEM) {}

    public async Task<bool> HasRegister()
    {
        Item? doc = await _collection.Find(Filter.Empty).FirstOrDefaultAsync();
        return doc != null;
    }
}
