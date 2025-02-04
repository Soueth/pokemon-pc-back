using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.DTOs;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;
using PokemonPc.Repositories;

namespace PokemonPc.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(IMongoDatabase db) 
        : base(db, AppConstants.PROVIDERS.USER) 
    { 
        var indexModel = new CreateIndexModel<User>(
            Builders<User>.IndexKeys.Ascending(doc => doc.PersonalToken)
        );

        _collection.Indexes.CreateOne(indexModel);
    }

    public async Task<User?> getWithPassword(string email)
    {
        // var projection = Projection
        //     .Exclude(u => u.Id)
        //     .Include(u => u.PersonalToken)
        //     .Include(u => u.Password);

        return await _collection.Find(Filter.Eq("email", email))
                                // .Project<LoginDto>(projection)
                                .FirstOrDefaultAsync();
    }

    public async Task<bool> verifyEmail(string email)
    {
        return await _collection.Find(Filter.Eq("email", email)).AnyAsync();
    }
}