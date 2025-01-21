using PokemonPc.Models;

namespace PokemonPc.Interfaces.Services;

public interface ITrainerService
{
    Task<Trainer> CreateTrainer(Trainer trainer);
}
