using MongoDB.Bson.Serialization.Attributes;

namespace PokemonPc.Models;

public class Ability : Model
{
    [BsonElement("externalId")]
    public int ExtenalId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("description")]
    public string Description { get; set; } = null!;
    
    [BsonElement("effect")]
    public string Effect { get; set; } = null!;
}
