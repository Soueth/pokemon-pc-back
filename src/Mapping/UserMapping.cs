using MongoDB.Bson;
using PokemonPc.DTOs;
using PokemonPc.Models;

namespace PokemonPc.Mapping;

public static class UserMapping
{
    public static User ToEntity(this CreateUserDto user)
    {
        // ObjectId trainerId;

        // if (!ObjectId.TryParse(user.Trainer, out trainerId))
        // {
        //     throw new ArgumentException($"Id inválido da entidade {nameof(user.Trainer)}");
        // }

        return new()
        {
            Email = user.Email,
            // Trainer = new Trainer() { Id = trainerId },
        };
    }
}