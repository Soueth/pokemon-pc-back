namespace PokemonPc.Utils.Exceptions;

public class NotFoundElementException : Exception
{
    public NotFoundElementException(string array, string element)
        : base($"Elemento {element} não encontrado no array {array}") { }
}
