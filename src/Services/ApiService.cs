using System.Text.Json;
using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.Services;
using PokemonPc.Models;
using PokemonPc.Utils.Functions;

namespace PokemonPc.Services;

public class ApiService : IApiService
{
    // HttpClient e API
    private readonly HttpClient _httpClient;
    private readonly string POKE_API;

    // Collections
    private readonly IMongoCollection<Pokedex> _pokedexCollection;
    private readonly IMongoCollection<Move> _moveCollection;
    private readonly IMongoCollection<Ability> _abilityCollection;
    private readonly IMongoCollection<Item> _itemCollection;

    // Contador de objetos possídos
    private Dictionary<string, int> _counts = [];

    public ApiService
    (
        IMongoDatabase db,
        HttpClient httpClient,
        IConfiguration configuration
    )
    {
        _pokedexCollection = db.GetCollection<Pokedex>(APP_CONSTANTS.PROVIDERS.POKEDEX);
        _moveCollection = db.GetCollection<Move>(APP_CONSTANTS.PROVIDERS.MOVE);
        _abilityCollection = db.GetCollection<Ability>(APP_CONSTANTS.PROVIDERS.ABILITY);
        _itemCollection = db.GetCollection<Item>(APP_CONSTANTS.PROVIDERS.ITEM);

        _httpClient = httpClient;
        POKE_API = configuration["PokeApi"]!;

        VerifyOwnData();
    }

    private async Task<Dictionary<string, int>> VerifyExternalData()
    {
        // Faz requisições da quantidade dos obj faltantes
        HttpResponseMessage taskAbilities = await _httpClient.GetAsync($"{POKE_API}/abilities");
        HttpResponseMessage taskMoves = await _httpClient.GetAsync($"{POKE_API}/move");
        HttpResponseMessage taskItems = await _httpClient.GetAsync($"{POKE_API}/item");
        HttpResponseMessage taskPokemon = await _httpClient.GetAsync($"{POKE_API}/pokemon");

        // string abilitiesBody = await taskAbilities.Content.ReadAsStringAsync();
        // var abilities = JsonSerializer.Deserialize<ResponseCount>(abilitiesBody);
        
        // Organiza as respostas
        ResponseCount? abilities = await JsonFunctions.ProcessResponse<ResponseCount>(taskAbilities);
        ResponseCount? moves = await JsonFunctions.ProcessResponse<ResponseCount>(taskMoves);
        ResponseCount? items = await JsonFunctions.ProcessResponse<ResponseCount>(taskItems);
        ResponseCount? pokemon = await JsonFunctions.ProcessResponse<ResponseCount>(taskPokemon);

        // string movesBody = await taskMoves.Content.ReadAsStringAsync();
        // var moves = JsonSerializer.Deserialize<ResponseCount>(movesBody);

        // string itemsBody = await taskItems.Content.ReadAsStringAsync();
        // var items = JsonSerializer.Deserialize<ResponseCount>(itemsBody);

        // string pokemonBody = await taskPokemon.Content.ReadAsStringAsync();
        // var pokemon = JsonSerializer.Deserialize<ResponseCount>(pokemonBody);

        // Retorna o dicionário com elas
        return new Dictionary<string, int> { 
            { "abilities", abilities?.Count ?? 0 },
            { "moves", moves?.Count ?? 0 },
            { "items", items?.Count ?? 0 },
            { "pokemon", pokemon?.Count ?? 0 },
        };
    }

    public async void VerifyOwnData()
    {
        Dictionary<string, int> counts = await VerifyExternalData();

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

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Banco de dados populado");
        Console.ResetColor();

        await Task.WhenAll(tasks);
    }

    public async Task PopulateAbilities(int id = 1)
    {
        await _httpClient.GetAsync($"{POKE_API}/abilities/{id}");

        return;
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
