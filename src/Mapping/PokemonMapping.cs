using PokemonPc.DTOs;
using PokemonPc.Models;
using PokemonPc.Utils.Exceptions;

namespace PokemonPc.Mapping;

public static class PokemonMapping
{
    public static PokemonBoxDto ToBoxDto(this Pokemon pokemon)
    {
        if (pokemon.Entry == null)
        {
            throw new EmptyEntityException
            (
                nameof(Pokemon), 
                nameof(Pokemon.Entry), 
                nameof(ToBoxDto)
            );
        }

        return new(
            pokemon.Id.ToString()!,
            pokemon.Nickname,
            pokemon.Entry.Name,
            pokemon.Entry.Image,
            pokemon.Entry.Types,
            pokemon.Level
        );
    }
}