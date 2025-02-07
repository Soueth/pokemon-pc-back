using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonPc.Models;

public class Item
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
}
