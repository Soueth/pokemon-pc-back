using PokemonPc.Models;

namespace PokemonPc.Interfaces.Repositories;

public interface IAbilityRepository : IRepository<Ability>
{
    Task<bool> HasRegister();
}
