using System;
using PokemonPc.Models;

namespace PokemonPc.Interfaces.Repositories;

public interface IPokedexRepository : IRepository<Pokedex>
{
    Task<bool> IsCollectionEmpty();
}
