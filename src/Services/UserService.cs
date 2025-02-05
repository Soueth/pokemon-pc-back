using System.Text.Json;
using MongoDB.Bson;
using PokemonPc.Constants;
using PokemonPc.DTOs;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Interfaces.Services;
using PokemonPc.Interfaces.Utils;
using PokemonPc.Mapping;
using PokemonPc.Models;
using PokemonPc.Utils.Exceptions;
using PokemonPc.Utils.Functions;
using PokemonPc.Utils.Types;

namespace PokemonPc.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITrainerService _trainerService;
    private readonly IAuthService _authService;

    private readonly IWebHostEnvironment _env;


    public UserService
    (
        IUserRepository userRepository, 
        ITrainerService trainerService, 
        IAuthService authService, 
        IWebHostEnvironment env
    ) {
        _userRepository = userRepository;
        _trainerService = trainerService;
        _authService = authService;
        _env = env;
    }

    public async Task<UserTokenDto> CreateUser(CreateUserDto user)
    {
        if (await _userRepository.verifyEmail(user.Email))
        {
            throw new EmailJaCadastradoException(user.Email);
        }

        Trainer trainer = await _trainerService.CreateTrainer(
            new() { Name = user.Name }
        );

        User userEntity = user.ToEntity(trainer);
        ObjectId _id = ObjectId.GenerateNewId();

        userEntity.Id = _id;
        userEntity.PersonalToken = UuidGenerator.GenerateUuid(APP_CONSTANTS.USERS_GUID, _id.ToString());
        userEntity.Password = Encrypter.HashPassword(user.Password);

        Task<User> promise = _userRepository.CreateAsync(userEntity);

        string token = _authService.GenerateToken(userEntity.PersonalToken.ToString()!);

        User _user = await promise;
        return _user.ToTokenDto(token);
    }

    public User GetById(MongoId _id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        User? user = await _userRepository.getWithPassword(loginDto.Email);

        if (user == null || !Encrypter.VerifyPassword(loginDto.Password, user.Password))
        {
            throw new EmailSenhaIncorretosException();
        }

        return _authService.GenerateToken(user.PersonalToken.ToString()!);
    }
}