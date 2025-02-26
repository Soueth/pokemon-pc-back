using System.Net;
using Microsoft.OpenApi.Any;

namespace PokemonPc.Utils.Exceptions;

public class CustomHttpRequestException : HttpRequestException
{
    public CustomHttpRequestException(string url, HttpStatusCode statusCode, HttpContent content)
        : base($"A requisição a {url} falhou.\n {content}", null, statusCode) {}
}
