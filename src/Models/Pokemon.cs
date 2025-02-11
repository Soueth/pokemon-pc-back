using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonPc.Models;

public class Pokemon : Model
{
    // public ICollection<Pokedex> Entry { get; set; } = null!;
    public string Nickname { get; set; } = null!;


    // TODO: implementar a foreign key para item
    [BsonElement("item")]
    public ObjectId? ItemId { get; set; }
    [BsonIgnore]
    public Item? Item { get; set; }


    // TODO: implementar a foreign key para move
    [BsonElement("move")]
    public ObjectId? MoveId { get; set; }
    [BsonIgnore]
    public Move? Move { get; set; }


    // TODO: implementar a foreign key para ability
    [BsonElement("ability")]
    public ObjectId? AbilityId { get; set; }
    [BsonIgnore]
    public Ability? Ability { get; set; }


    // TODO: implementar a foreign key para trainer
    [BsonElement("trainer")]
    public ObjectId? TrainerId { get; set; }
    [BsonIgnore]
    public Trainer? Trainer { get; set; }


    [BsonElement("entry")]
    public ObjectId? EntryId { get; set; }
    [BsonIgnore]
    public Pokedex? Entry { get; set; }


    [BsonElement("level")]
    public int Level { get; set; }


    [BsonElement("box")]
    public int Box { get; set; }


    [BsonElement("boxPosition")]
    public int BoxPosition { get; set; }
}
