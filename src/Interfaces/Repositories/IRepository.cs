using PokemonPc.Models;
using PokemonPc.Utils.Types;

namespace PokemonPc.Interfaces.Repositories;

public interface IRepository<T> where T : Model
{
    Task<T> GetByIdAsync(MongoId id);
    // Task<T?> HasRegister();
    Task<T> CreateAsync(T entity);
    Task<T[]> CreateManyAsync(IEnumerable<T> entity, bool generateId);
    Task<bool> UpdateByIdAsync(MongoId id, T entity);
    Task DeleteAsync(MongoId id);
}
