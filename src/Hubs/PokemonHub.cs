using Microsoft.AspNetCore.SignalR;
using PokemonPc.Interfaces.Repositories;

namespace PokemonPc.Hubs;

public class PokemonHub : Hub
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonHub(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("ReceiveMessage", "Pokemon Hub Connected!");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Clients.Caller.SendAsync("ReceiveMessage", "Pokemon Hub Disconnected!");
        await base.OnDisconnectedAsync(exception);
    }

    public async Task RequestBoxes()
    {
        // List<PokemonBoxDto> data = _pokemonRepository.
    }
}
