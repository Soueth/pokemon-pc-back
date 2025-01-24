using PokemonPc.DTOs;
using PokemonPc.Models;

namespace PokemonPc.Mapping;

public static class UserMapping
{
    public static User ToEntity(this CreateUserDto user, Trainer trainer)
    {
        // ObjectId trainerId;

        // if (!ObjectId.TryParse(user.Trainer, out trainerId))
        // {
        //     throw new ArgumentException($"Id inválido da entidade {nameof(user.Trainer)}");
        // }

        return new()
        {
            Email = user.Email,
            TrainerId = trainer.Id,
            Trainer = trainer,
        };
    }

    public static UserDto ToDto(this User user, string name)
    {
        if (!user.Id.HasValue)
        {
            throw new ArgumentException($"Id da entidade {nameof(user)} não foi preenchido");
        }

        return new(
            user.Id.ToString()!,
            name,
            user.Email,
            user.TrainerId.ToString()!,
            user.CreatedAt,
            user.UpdatedAt
        );
    }

    public static UserTokenDto ToTokenDto(this User user, string token)
    {
        if (!user.Id.HasValue)
        {
            throw new ArgumentException($"Id da entidade {nameof(user)} não foi preenchido");
        }

        return new(
            user.Id.ToString()!,
            token
        );
    }
}