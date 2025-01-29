namespace PokemonPc.Utils.Exceptions;

public class EmptyIdException : Exception
{
    public EmptyIdException(string entity)
        : base($"Id da entidade {entity} não foi preenchido"){ }
}
