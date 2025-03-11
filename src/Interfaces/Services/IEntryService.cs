namespace PokemonPc.Interfaces.Services;

public interface IEntryService
{
    void VerifyCollection();
    Task PopulateEntry();
}
