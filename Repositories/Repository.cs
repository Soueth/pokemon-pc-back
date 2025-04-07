using System;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;
using PokemonPc.Utils.Types;

namespace PokemonPc.Repositories;

public abstract class Repository<T> : IRepository<T> where T : Model
{
    protected readonly IMongoCollection<T> _collection;
    protected FilterDefinitionBuilder<T> Filter { get; }
    protected ProjectionDefinitionBuilder<T> Projection { get; }

    protected Repository(IMongoDatabase db, string collectionName)
    {
        _collection = db.GetCollection<T>(collectionName);
        Filter = Builders<T>.Filter;
        Projection = Builders<T>.Projection;
    }

    public async Task<T> GetByIdAsync(MongoId id)
    {
        return await _collection.Find(
            Filter.Eq("_id", id.ToObjectId)
        ).FirstOrDefaultAsync();
    }

    // public async Task<T?> HasRegister()
    // {
    //     return await _collection.Find(Filter.Empty).FirstOrDefaultAsync();
    // }
    
    public async Task<T> CreateAsync(T entity)
    {
        entity.Id = ObjectId.GenerateNewId();
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<T[]> CreateManyAsync(IEnumerable<T> entities, bool generateId = true)
    {
        if (generateId)
        {
            foreach (var entity in entities)
            {
                entity.Id = ObjectId.GenerateNewId();
            }
        }

        await _collection.InsertManyAsync(entities);
        return entities.ToArray();
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
