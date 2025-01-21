using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonPc.Models;

public class BaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public ObjectId? Id { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    [BsonDefaultValue("new Date()")]
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}
