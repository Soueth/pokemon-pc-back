using MongoDB.Bson;
using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.DTOs;
using PokemonPc.Hubs;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;

namespace PokemonPc.Repositories;

public class PokemonRepository : Repository<Pokemon>, IPokemonRepository
{
    public PokemonRepository(IMongoDatabase db)
        : base(db, APP_CONSTANTS.PROVIDERS.POKEMON) { }

    public async Task<PokemonBoxDto[]> GetFromBox(int box = 0)
    {
        var pipeline = _collection.Aggregate()
            .Match(doc => doc.Box == box)
            .Lookup(APP_CONSTANTS.PROVIDERS.POKEDEX, "entry", "_id", "entry")
            .Sort("{ box: 1 }")
            .As<Pokemon>();

        var result = await pipeline.ToListAsync();

        return result?.Select(doc => new PokemonBoxDto(
            doc.Id.ToString()!,
            doc.Nickname!,
            doc.Entry?.Name!,
            doc.Entry?.Image!,
            doc.Entry?.Types!,
            doc.Level
        )).ToArray() ?? Array.Empty<PokemonBoxDto>();
    }
}
