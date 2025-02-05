namespace PokemonPc.Interfaces.Services;

public interface IPokedexService
{
    Task VerifyCollection();
    Task PopulatePokedex();
}
