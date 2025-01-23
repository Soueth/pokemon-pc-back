using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;
using PokemonPc.Repositories;

namespace PokemonPc.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(IMongoDatabase db) 
        : base(db, AppConstants.PROVIDERS.USER) { }

    public async Task<bool> verifyEmail(string email)
    {
        return await _collection.Find(Filter.Eq("email", email)).AnyAsync();
    }
}