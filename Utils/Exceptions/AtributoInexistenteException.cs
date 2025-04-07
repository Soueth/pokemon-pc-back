namespace PokemonPc.Utils.Exceptions;

public class AtributoInexistenteException : Exception 
{
    public AtributoInexistenteException(string attr, string obj)
        : base($"Atributo {attr} não existente no objeto {obj}") {}
}