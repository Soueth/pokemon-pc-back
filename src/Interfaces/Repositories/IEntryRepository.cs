using PokemonPc.Models;

namespace PokemonPc.Interfaces.Repositories;

public interface IEntryRepository : IRepository<Entry>
{
    Task<bool> IsCollectionEmpty();
}
