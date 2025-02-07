using System.Net.Http;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Interfaces.Services;

namespace PokemonPc.Services;

public class PokedexService : IPokedexService
{
    private readonly IPokedexRepository _pokedexRepository;

    public PokedexService
    (
        IPokedexRepository pokedexRepository
    )
    {
        _pokedexRepository = pokedexRepository;

        // VerifyCollection();
    }

    public Task PopulatePokedex()
    {
        // Implement logic to populate the pokedex with data from PokeAPI.
        // This method should be called when the pokedex is empty or needs to be updated.
        // For example, you could fetch new data from a PokeAPI or a database containing the latest Pokémon.


        return Task.CompletedTask;
    }

    public async void VerifyCollection()
    {
        bool isEmpty = await _pokedexRepository.IsCollectionEmpty();

        if (isEmpty)
        {
            await PopulatePokedex();
        }
    }
}
