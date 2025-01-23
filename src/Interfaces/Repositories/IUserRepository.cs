using PokemonPc.DTOs;
using PokemonPc.Models;

namespace PokemonPc.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    // User create(CreateUserDto user);
    Task<bool> verifyEmail(string email);
}
