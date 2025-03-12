using PokemonPc.Models;

namespace PokemonPc.Interfaces.Repositories;

public interface IItemRepository : IRepository<Item>
{
    Task<bool> HasRegister();
}
