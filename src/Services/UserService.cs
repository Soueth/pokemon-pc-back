using MongoDB.Bson;
using PokemonPc.DTOs;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Interfaces.Services;
using PokemonPc.Mapping;
using PokemonPc.Models;
using PokemonPc.Utils.Types;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void CreateUser(CreateUserDto user)
    {
        

        _userRepository.CreateAsync(user.ToEntity());
    }

    public User GetById(MongoId _id)
    {
        throw new NotImplementedException();
    }
}