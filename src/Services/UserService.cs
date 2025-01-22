using System.Text.Json;
using MongoDB.Bson;
using PokemonPc.DTOs;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Interfaces.Services;
using PokemonPc.Mapping;
using PokemonPc.Models;
using PokemonPc.Utils.Types;

namespace PokemonPc.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITrainerService _trainerService;

    public UserService(IUserRepository userRepository, ITrainerService trainerService)
    {
        _userRepository = userRepository;
        _trainerService = trainerService;
    }

    public async Task<User> CreateUser(CreateUserDto user)
    {
        Trainer trainer = await _trainerService.CreateTrainer(
            new() { Name = user.Name }
        );

        return await _userRepository.CreateAsync
        (
            user.ToEntity(trainer)
        );
    }

    public User GetById(MongoId _id)
    {
        throw new NotImplementedException();
    }
}