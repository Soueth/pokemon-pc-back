using System;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;

namespace PokemonPc.Repositories;

public class PokedexRepository : Repository<Pokedex>, IPokedexRepository
{
    public PokedexRepository(IMongoDatabase db) 
        : base(db, APP_CONSTANTS.PROVIDERS.POKEDEX) {}

    public async Task<bool> IsCollectionEmpty()
    {
        Pokedex? doc = await _collection.Find(FilterDefinition<Pokedex>.Empty)
                                    .Limit(1)
                                    .FirstOrDefaultAsync();

        return doc == null;
    }
}
