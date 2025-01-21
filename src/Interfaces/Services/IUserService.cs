using System;
using PokemonPc.DTOs;
using PokemonPc.Models;
using PokemonPc.Utils.Types;

namespace PokemonPc.Interfaces.Services;

public interface IUserService
{
    User GetById(MongoId _id);
    void CreateUser(CreateUserDto user);
}
