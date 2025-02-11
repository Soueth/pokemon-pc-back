using System;
using PokemonPc.DTOs;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Models;

namespace PokemonPc.Interfaces.Repositories;

public interface IPokemonRepository : IRepository<Pokemon>
{
    Task<PokemonBoxDto[]> GetFromBox(int box);
}
