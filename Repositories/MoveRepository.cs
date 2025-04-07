using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;

namespace PokemonPc.Repositories;

public class MoveRepository : Repository<Move>, IMoveRepository
{
    public MoveRepository(IMongoDatabase db) 
        : base(db, APP_CONSTANTS.PROVIDERS.MOVE) {}

    public async Task<bool> HasRegister()
    {
        Move? doc = await _collection.Find(Filter.Empty).FirstOrDefaultAsync();
        return doc != null;
    }
}
