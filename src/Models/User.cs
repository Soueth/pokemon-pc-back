using System;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonPc.Models;

public class User : BaseModel
{
    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("trainer")]
    public ICollection<Trainer> Trainer { get; set; } = null!;

    // TODO: o atributo 'password' e a lógica de autentificação
}
