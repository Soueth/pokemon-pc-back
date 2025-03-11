using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PokemonPc.Utils.Types;

namespace PokemonPc.Models;

public class MovePokemon : Model
{
    [BsonElement("move")]
    public ObjectId MoveId { get; set; }

    [BsonIgnore]
    public ICollection<Move> Move { get; set; } = null!;

    [BsonElement("pokemon")]
    public ObjectId PokemonId { get; set; }

    [BsonIgnore]
    public ICollection<Entry> Pokemon { get; set; } = null!;

    public LearningMethod[] LearningMethod { get; set; } = null!;
    public int? LearningLevel { get; set; }
}
