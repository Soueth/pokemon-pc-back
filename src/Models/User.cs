using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonPc.Models;

public class User : BaseModel
{
    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("trainer")]
    public ObjectId? TrainerId { get; set; }

    [BsonIgnore]
    public Trainer? Trainer { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid? ConnectionString { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; } = null!;
}
