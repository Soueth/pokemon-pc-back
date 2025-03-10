using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonPc.Models;

public class Item : Model
{
    [BsonElement("externalId")]
    public int ExternalId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = null!;
    
    [BsonElement("description")]
    public string Description { get; set; } = null!;

    [BsonElement("effect")]
    public string Effect { get; set; } = null!;
    
    [BsonElement("imageUrl")]
    public string ImageUrl {get; set;} = null!;
}
