using PokemonPc.Interfaces.Repositories;
using PokemonPc.Interfaces.Services;

namespace PokemonPc.Services;

public class EntryService : IEntryService
{
    private readonly IEntryRepository _entryRepository;

    public EntryService
    (
        IEntryRepository entryRepository
    )
    {
        _entryRepository = entryRepository;

        // VerifyCollection();
    }

    public Task PopulateEntry()
    {
        // Implement logic to populate the entry with data from PokeAPI.
        // This method should be called when the entry is empty or needs to be updated.
        // For example, you could fetch new data from a PokeAPI or a database containing the latest Pokémon.


        return Task.CompletedTask;
    }

    public async void VerifyCollection()
    {
        bool isEmpty = await _entryRepository.IsCollectionEmpty();

        if (isEmpty)
        {
            await PopulateEntry();
        }
    }
}
