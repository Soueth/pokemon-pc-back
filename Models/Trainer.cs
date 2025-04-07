using MongoDB.Bson.Serialization.Attributes;

namespace PokemonPc.Models;

public class Trainer : Model
{
    [BsonElement("name")]
    public string Name { get; set; } = null!;
}