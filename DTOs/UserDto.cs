using System;

namespace PokemonPc.DTOs;

public record class UserDto(
    string Id,
    string Name,
    string Email,
    string TrainerId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
