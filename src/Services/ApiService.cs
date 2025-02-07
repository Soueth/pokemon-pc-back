using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Services;
using PokemonPc.Models;

namespace PokemonPc.Services;

public class ApiService : IApiService
{
    // HttpClient e API
    private readonly IHttpClientFactory _httpClient;
    private readonly string POKE_API;

    // Collections
    private readonly IMongoCollection<Pokedex> _pokedexCollection;
    private readonly IMongoCollection<Move> _moveCollection;
    private readonly IMongoCollection<Ability> _abilityCollection;
    private readonly IMongoCollection<Item> _itemCollection;

    // Contador de objetos possídos
    private Dictionary<string, int> counts = [];

    public ApiService
    (
        IMongoDatabase db, 
        IHttpClientFactory httpClient, 
        IConfiguration configuration
    ) 
    {
        _pokedexCollection = db.GetCollection<Pokedex>(APP_CONSTANTS.PROVIDERS.POKEDEX);
        _moveCollection = db.GetCollection<Move>(APP_CONSTANTS.PROVIDERS.MOVE);
        _abilityCollection = db.GetCollection<Ability>(APP_CONSTANTS.PROVIDERS.ABILITY);
        _itemCollection = db.GetCollection<Item>(APP_CONSTANTS.PROVIDERS.ITEM);

        _httpClient = httpClient;
        _httpClient = httpClient;
        POKE_API = configuration["PokeApi"]!;

        VerifyExternalData();
        VerifyOwnData();
    }

    private async void VerifyExternalData()
    {
        
    }

    public async void VerifyOwnData()
    {
        // Verifica quais collections não estão populadas
        Task<Item> hasItem = _itemCollection.Find(FilterDefinition<Item>.Empty)
                                                .Limit(1)
                                                .FirstOrDefaultAsync();

        Task<Ability> hasAbility = _abilityCollection.Find(FilterDefinition<Ability>.Empty)
                                                    .Limit(1)
                                                    .FirstOrDefaultAsync();

        Task<Move> hasMove = _moveCollection.Find(FilterDefinition<Move>.Empty)
                                            .Limit(1)
                                            .FirstOrDefaultAsync();

        Task<Pokedex> hasPokemon = _pokedexCollection.Find(FilterDefinition<Pokedex>.Empty)
                                                    .Limit(1)
                                                    .FirstOrDefaultAsync();

        // Aguarda todas as verificações terminarem e popula as coleções
        await Task.WhenAll(hasItem, hasAbility, hasMove, hasPokemon);

        // Popula as coleções
        List<Task> tasks = [];

        if ((await hasItem) != null) 
        {
            tasks.Add(PopulateItems());
        }
        if ((await hasAbility) != null)
        {
            tasks.Add(PopulateAbilities());
        }
        if ((await hasMove) != null)
        {
            tasks.Add(PopulateMoves());
        }
        if ((await hasPokemon) != null)
        {
            tasks.Add(PopulatePokemons());
        }
    }

    public Task PopulateAbilities(int id = 1)
    {
        return Task.CompletedTask;
    }

    public Task PopulateItems(int id = 1)
    {
        return Task.CompletedTask;
    }

    public Task PopulateMoves(int id = 1)
    {
        return Task.CompletedTask;
    }

    public Task PopulatePokemons(int id = 1)
    {
        return Task.CompletedTask;
    }
}
