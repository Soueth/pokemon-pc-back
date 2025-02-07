namespace PokemonPc.Interfaces.Services;

public interface IPokedexService
{
    void VerifyCollection();
    Task PopulatePokedex();
}
