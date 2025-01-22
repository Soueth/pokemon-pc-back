using System;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;
using PokemonPc.Utils.Types;

namespace PokemonPc.Repositories;

public class Repository<T> : IRepository<T> where T : BaseModel
{
    protected readonly IMongoCollection<T> _collection;
    private FilterDefinitionBuilder<T> Filter { get; }


    public Repository(IMongoDatabase db, string collectionName)
    {
        _collection = db.GetCollection<T>(collectionName);
        Filter = Builders<T>.Filter;
    }

    public async Task<T> GetByIdAsync(MongoId id)
    {
        return await _collection.Find(
            Filter.Eq("_id", id.ToObjectId)
        ).FirstOrDefaultAsync();
    }
    public async Task<T> CreateAsync(T entity)
    {
        entity.Id = ObjectId.GenerateNewId();
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<bool> UpdateByIdAsync(MongoId id, T entity)
    {
        entity.UpdatedAt = DateTime.Now;

        var result = await _collection.ReplaceOneAsync(
            Filter.Eq("_id", id.ToObjectId),
            entity
        );

        return result.MatchedCount > 0;
    }

    public async Task DeleteAsync(MongoId id)
    {
        await _collection.DeleteOneAsync(
            Filter.Eq("_id", id.ToObjectId)
        );
    }
}
