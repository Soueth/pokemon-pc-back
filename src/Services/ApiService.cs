using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.External;
using PokemonPc.Interfaces.Services;
using PokemonPc.Mapping;
using PokemonPc.Models;
using PokemonPc.Utils.Exceptions;
using PokemonPc.Utils.Functions;

namespace PokemonPc.Services;

public class ApiService : IApiService
{
    // HttpClient e API
    private readonly HttpClient _httpClient;
    private readonly string POKE_API;

    // Collections
    private readonly IMongoCollection<Entry> _pokedexCollection;
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
        _pokedexCollection = db.GetCollection<Entry>(APP_CONSTANTS.PROVIDERS.POKEDEX);
        _moveCollection = db.GetCollection<Move>(APP_CONSTANTS.PROVIDERS.MOVE);
        _abilityCollection = db.GetCollection<Ability>(APP_CONSTANTS.PROVIDERS.ABILITY);
        _itemCollection = db.GetCollection<Item>(APP_CONSTANTS.PROVIDERS.ITEM);

        _httpClient = httpClient;
        POKE_API = configuration["PokeApi"]!;

        VerifyOwnData();
    }

    private async Task<Dictionary<string, int>> CountExternalData()
    {
        // Faz requisições da quantidade dos obj faltantes
        HttpResponseMessage taskAbilities = await _httpClient.GetAsync($"{POKE_API}/abilities");
        HttpResponseMessage taskMoves = await _httpClient.GetAsync($"{POKE_API}/move");
        HttpResponseMessage taskItems = await _httpClient.GetAsync($"{POKE_API}/item");
        HttpResponseMessage taskPokemon = await _httpClient.GetAsync($"{POKE_API}/pokemon");

        // string abilitiesBody = await taskAbilities.Content.ReadAsStringAsync();
        // var abilities = JsonSerializer.Deserialize<ResponseCount>(abilitiesBody);
        
        ResponseCount? abilities = null;
        ResponseCount? moves = null;
        ResponseCount? items = null;
        ResponseCount? pokemon = null;
        // Organiza as respostas
        if (taskAbilities.IsSuccessStatusCode) 
        {
            abilities = await taskAbilities.Content.ReadFromJsonAsync<ResponseCount>();
        }

        if (taskMoves.IsSuccessStatusCode) 
        {
            moves = await taskMoves.Content.ReadFromJsonAsync<ResponseCount>();
        }

        if (taskItems.IsSuccessStatusCode) 
        {
            items = await taskItems.Content.ReadFromJsonAsync<ResponseCount>();
        }

        if (taskPokemon.IsSuccessStatusCode) 
        {
            pokemon = await taskPokemon.Content.ReadFromJsonAsync<ResponseCount>();
        }

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
        Dictionary<string, int> counts = await CountExternalData();

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

        Task<Entry> hasPokemon = _pokedexCollection.Find(FilterDefinition<Entry>.Empty)
                                                    .Limit(1)
                                                    .FirstOrDefaultAsync();

        // Aguarda todas as verificações terminarem e popula as coleções
        await Task.WhenAll(hasItem, hasAbility, hasMove, hasPokemon);

        // Popula as coleções
        List<Task> tasks = [];

        if ((await hasAbility) != null)
        {
            tasks.Add(PopulateAbilities(counts["abilities"]));
        }
        if ((await hasItem) != null)
        {
            tasks.Add(PopulateItems(counts["items"]));
        }
        if ((await hasMove) != null)
        {
            tasks.Add(PopulateMoves(counts["moves"]));
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Banco de dados populado");
        Console.ResetColor();

        await Task.WhenAll(tasks);

        if ((await hasPokemon) != null)
        {
            await PopulatePokemons(counts["pokemon"]);
        }
    }

    public async Task PopulateAbilities(int qtd)
    {
        for (int i = 1; i < qtd; i++) {
            string url = $"{POKE_API}/ability/{i}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) 
            {
                throw new CustomHttpRequestException(url, response.StatusCode, response.Content);
            }
    
            ApiAbility? data = await response.Content.ReadFromJsonAsync<ApiAbility>();

            if (data == null)
            {
                throw new ArgumentNullException();
            }
            
            Ability ability = data.ToAbility();

            ability.Id = ObjectId.GenerateNewId();
            await _abilityCollection.InsertOneAsync(ability);
        }

        return;
    }

    public async Task PopulateItems(int qtd)
    {
        for (int i = 1; i < qtd; i++) {
            string url = $"{POKE_API}/item/{i}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) 
            {
                throw new CustomHttpRequestException(url, response.StatusCode, response.Content);
            }
    
            ApiItem? data = await response.Content.ReadFromJsonAsync<ApiItem>();

            if (data == null)
            {
                throw new ArgumentNullException();
            }
            
            Item item = data.ToItem();

            item.Id = ObjectId.GenerateNewId();
            await _itemCollection.InsertOneAsync(item);
        }

        return;
    }

    public async Task PopulateMoves(int qtd)
    {
        for (int i = 1; i < qtd; i++) {
            string url = $"{POKE_API}/move/{i}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) 
            {
                throw new CustomHttpRequestException(url, response.StatusCode, response.Content);
            }
    
            ApiMove? data = await response.Content.ReadFromJsonAsync<ApiMove>();

            if (data == null)
            {
                throw new ArgumentNullException();
            }
            
            Move move = data.ToMove();

            move.Id = ObjectId.GenerateNewId();
            await _moveCollection.InsertOneAsync(move);
        }

        return;
    }

    public Task PopulatePokemons(int qtd)
    {
        return Task.CompletedTask;
    }
}
