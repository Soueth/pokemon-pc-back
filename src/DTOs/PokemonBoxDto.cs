namespace PokemonPc.DTOs;

public record class PokemonBoxDto(
    string _id,
    string nickname,
    string name,
    string image,
    string[] type,
    int level
);
