using PokemonPc.Interfaces.Repositories;
using PokemonPc.Interfaces.Services;
using PokemonPc.Models;

namespace PokemonPc.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepository;

    public TrainerService(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }

    public async Task<Trainer> CreateTrainer(Trainer trainer)
    {
        return await _trainerRepository.CreateAsync(trainer);
    }
}
