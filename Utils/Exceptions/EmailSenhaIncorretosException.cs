namespace PokemonPc.Utils.Exceptions;

public class EmailSenhaIncorretosException : Exception
{ 
    public EmailSenhaIncorretosException()
        :base("Email e/ou senha inválido(s)") {}
}
