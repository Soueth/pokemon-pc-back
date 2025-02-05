using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;

namespace PokemonPc.Repositories;

public class TrainerRepository : Repository<Trainer>, ITrainerRepository
{
    public TrainerRepository(IMongoDatabase db)
        : base(db, APP_CONSTANTS.PROVIDERS.TRAINER) { }
}
