using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace PokemonPc.Models;

[Collection("users")]
public class User : Model
{
    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("trainer")]
    public ObjectId? TrainerId { get; set; }

    [BsonIgnore]
    public Trainer? Trainer { get; set; }

    // [BsonRepresentation(BsonType.String)]
    [BsonElement("personalToken")]
    public Guid? PersonalToken { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; } = null!;
}
