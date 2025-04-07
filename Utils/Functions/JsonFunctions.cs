using System.Text.Json;

namespace PokemonPc.Utils.Functions;

public class JsonFunctions
{
    public static async Task<T?> ProcessResponse<T>(HttpResponseMessage task)
    {
        string body = await task.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(body);
    }
}
