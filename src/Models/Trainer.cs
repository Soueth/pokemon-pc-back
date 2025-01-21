using MongoDB.Bson.Serialization.Attributes;

namespace PokemonPc.Models;

public class Trainer : BaseModel
{
    [BsonElement("name")]
    public string Name { get; set; } = null!;
}