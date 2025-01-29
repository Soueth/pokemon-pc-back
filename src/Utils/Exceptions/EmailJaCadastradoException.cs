namespace PokemonPc.Utils.Exceptions;

public class EmailJaCadastradoException : Exception
{
    public EmailJaCadastradoException(string email)
        : base($"Email {email} já cadastrado") {}
}
