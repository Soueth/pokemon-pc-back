using PokemonPc.Models;
using PokemonPc.Utils.Types;

namespace PokemonPc.Interfaces.Repositories;

public interface IRepository<T> where T : BaseModel
{
    Task<T> GetByIdAsync(MongoId id);
    Task<T> CreateAsync(T entity);
    Task<bool> UpdateByIdAsync(MongoId id, T entity);
    Task DeleteAsync(MongoId id);
}
