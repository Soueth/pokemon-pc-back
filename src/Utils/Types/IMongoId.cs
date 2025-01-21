using System;
using MongoDB.Bson;

namespace PokemonPc.Utils.Types;

public class MongoId
{
    public string StringId { get; set; } = null!;
    public ObjectId? ObjectId { get; set; }

    public MongoId(string _id)
    {
        StringId = _id;
    }

    public MongoId(ObjectId _id)
    {
        ObjectId = _id;
    }

    public bool IsString => StringId != null;
    public bool IsObjectId => ObjectId.HasValue;

    public bool HasValue => IsString || IsObjectId;

    public ObjectId ToObjectId => ObjectId ?? new ObjectId(StringId);
}
