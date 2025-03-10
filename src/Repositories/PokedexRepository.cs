using System;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;

namespace PokemonPc.Repositories;

public class PokedexRepository : Repository<Entry>, IPokedexRepository
{
    public PokedexRepository(IMongoDatabase db) 
        : base(db, APP_CONSTANTS.PROVIDERS.POKEDEX) {}

    public async Task<bool> IsCollectionEmpty()
    {
        Entry? doc = await _collection.Find(FilterDefinition<Entry>.Empty)
                                    .Limit(1)
                                    .FirstOrDefaultAsync();

        return doc == null;
    }
}
