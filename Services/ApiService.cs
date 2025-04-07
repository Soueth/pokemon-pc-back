using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonPc.Constants;
using PokemonPc.Interfaces.External;
using PokemonPc.Interfaces.Repositories;
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
    // private readonly IMongoCollection<Move> _moveCollection;
    private readonly IMoveRepository _moveRepository;
    // private readonly IMongoCollection<Ability> _abilityCollection;
    private readonly IAbilityRepository _abilityRepository;
    // private readonly IMongoCollection<Item> _itemCollection;
    private readonly IItemRepository _itemRepository;
    // private readonly IMongoCollection<Entry> _entryCollection;
    private readonly IEntryRepository _entryRepository;
    // private readonly IMongoCollection<MovePokemon> _movePokemonCollection;
    private readonly IMovePokemonRepository _movePokemonRepository;
    // private readonly IMongoCollection<AbilityPokemon> _abilityPokemonCollection;
    private readonly IAbilityPokemonRepository _abilityPokemonRepository;

    // Contador de objetos possídos
    private Dictionary<string, int> _counts = [];

    public ApiService
    (
        IMongoDatabase db,
        HttpClient httpClient,
        IConfiguration configuration,
        IMoveRepository moveRepository,
        IAbilityRepository abilityRepository,
        IItemRepository itemRepository,
        IEntryRepository entryRepository,
        IMovePokemonRepository movePokemonRepository,
        IAbilityPokemonRepository abilityPokemonRepository
    )
    {
        // _moveCollection = db.GetCollection<Move>(APP_CONSTANTS.PROVIDERS.MOVE);
        _moveRepository = moveRepository;
        // _abilityCollection = db.GetCollection<Ability>(APP_CONSTANTS.PROVIDERS.ABILITY);
        _abilityRepository = abilityRepository;
        // _itemCollection = db.GetCollection<Item>(APP_CONSTANTS.PROVIDERS.ITEM);
        _itemRepository = itemRepository;
        // _entryCollection = db.GetCollection<Entry>(APP_CONSTANTS.PROVIDERS.ENTRY);
        _entryRepository = entryRepository;
        // _movePokemonCollection = db.GetCollection<MovePokemon>(APP_CONSTANTS.PROVIDERS.MOVE_POKEMON);
        _movePokemonRepository = movePokemonRepository;
        // _abilityPokemonCollection = db.GetCollection<AbilityPokemon>(APP_CONSTANTS.PROVIDERS.ABILITY_POKEMON);
        _abilityPokemonRepository = abilityPokemonRepository;

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
        Task<bool> hasItem = _itemRepository.HasRegister();
        Task<bool> hasAbility = _abilityRepository.HasRegister();
        Task<bool> hasMove = _moveRepository.HasRegister();
        Task<bool> hasPokemon = _entryRepository.HasRegister();

        // Aguarda todas as verificações terminarem e popula as coleções
        await Task.WhenAll(hasItem, hasAbility, hasMove, hasPokemon);

        // Popula as coleções
        List<Task> tasks = [];

        if (await hasAbility)
        {
            tasks.Add(PopulateAbilities(counts["abilities"]));
        }
        if (await hasItem)
        {
            tasks.Add(PopulateItems(counts["items"]));
        }
        if (await hasMove)
        {
            tasks.Add(PopulateMoves(counts["moves"]));
        }

        await Task.WhenAll(tasks);

        if (await hasPokemon)
        {
            await PopulatePokemons(counts["pokemon"]);
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Banco de dados populado");
        Console.ResetColor();
    }

    public async Task PopulateAbilities(int qtd)
    {
        Queue<Ability> abilities = [];
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

            abilities.Append(ability);

            if (i % 5 == 0 || i == (qtd - 1))
            {
                await _abilityRepository.CreateManyAsync(abilities, false);
                abilities.Clear();
            }
        }

        return;
    }

    public async Task PopulateItems(int qtd)
    {
        Queue<Item> items = [];
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

            items.Append(item);

            if (i % 5 == 0 || i == (qtd - 1))
            {
                await _itemCollection.InsertOneAsync(item);
            }
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

    public async Task PopulatePokemons(int qtd)
    {
        for (int i = 1; i < qtd; i++) {
            string url = $"{POKE_API}/pokemon/{i}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) 
            {
                throw new CustomHttpRequestException(url, response.StatusCode, response.Content);
            }
    
            ApiPokemon? data = await response.Content.ReadFromJsonAsync<ApiPokemon>();

            if (data == null)
            {
                throw new ArgumentNullException();
            }
            
            Entry entry = data.ToEntry();

            List<Task> tasks = [];

            entry.Id = ObjectId.GenerateNewId();
            tasks.Add(_entryCollection.InsertOneAsync(entry));

            foreach (var move in data.Moves)
            {
                MovePokemon movePokemon = move.ToMovePokemon()

                MovePokemon movePokemon = new()
                {
                    MoveId = move.Move.Id,
                    Pokemon = entry.Id,
                    Level = move.VersionGroupDetails[0].LevelLearnedAt,
                };

                tasks.Add(_movePokemonCollection.InsertOneAsync(movePokemon));
            }
        }

        return;
    }

    private GetMovesFromDatabase()
    {

    }
}
