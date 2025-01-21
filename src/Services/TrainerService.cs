using System;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Repositories;

namespace PokemonPc.Services;

public class TrainerService
{
    private readonly ITrainerRepository _trainerRepository;

    public TrainerService(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }
}
