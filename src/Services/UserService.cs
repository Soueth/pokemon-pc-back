using System.Text.Json;
using MongoDB.Bson;
using PokemonPc.Constants;
using PokemonPc.DTOs;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Interfaces.Services;
using PokemonPc.Mapping;
using PokemonPc.Models;
using PokemonPc.Utils.Functions;
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
        if (await _userRepository.verifyEmail(user.Email))
        {
            throw new ArgumentException($"Email {user.Email} já cadastrado");
        }

        Trainer trainer = await _trainerService.CreateTrainer(
            new() { Name = user.Name }
        );

        User userEntity = user.ToEntity(trainer);
        
        ObjectId _id = ObjectId.GenerateNewId();
        userEntity.Id = _id;

        userEntity.ConnectionString = UuidGenerator.GenerateUuid(AppConstants.USERS_GUID, _id.ToString());

        Task<User> promise = _userRepository.CreateAsync(userEntity);

        // TODO: implementar lógica para criação do token.

        return await promise;
    }

    public User GetById(MongoId _id)
    {
        throw new NotImplementedException();
    }
}