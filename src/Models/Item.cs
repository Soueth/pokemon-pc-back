using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonPc.Models;

public class Item
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = null!;
    
    [BsonElement("description")]
    public string Description { get; set; } = null!;

    [BsonElement("effect")]
    public string Effect { get; set; } = null!;
    
    [BsonElement("imageUrl")]
    public string ImageUrl {get; set;} = null!;
}
