using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;

namespace PokemonPc.Repositories;

public class MovePokemonRepository: Repository<MovePokemon>, IMovePokemonRepository
{
    public MovePokemonRepository(IMongoDatabase db)
        : base(db, APP_CONSTANTS.PROVIDERS.MOVE_POKEMON) {}
}
