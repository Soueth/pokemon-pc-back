using PokemonPc.Models;

namespace PokemonPc.Interfaces.Repositories;

public interface IMoveRepository: IRepository<Move>
{    
    Task<bool> HasRegister();
}
