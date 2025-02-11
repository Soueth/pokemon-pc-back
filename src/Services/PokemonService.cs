using System;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Interfaces.Services;

namespace PokemonPc.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }
}
