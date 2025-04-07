namespace PokemonPc.Utils.Exceptions;

public class EmptyEntityException : Exception
{
    public EmptyEntityException(string father, string entity, string method = "")
        : base($"{entity} de {father} está vazio. {getMethod(method)}") {}

    private static string getMethod(string method)
    {
        if (method != "")
        {
            method = $"na execução do método {method}";
        }
        return method;
    }
}
