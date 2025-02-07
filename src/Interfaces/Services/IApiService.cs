using System;

namespace PokemonPc.Interfaces.Services;

public interface IApiService
{
    public Task PopulateMoves(int id = 1);
    public Task PopulateItems(int id = 1);
    public Task PopulateAbilities(int id = 1);
    public Task PopulatePokemons(int id = 1);

    public void VerifyOwnData();
}
