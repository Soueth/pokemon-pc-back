using System;

namespace PokemonPc.src.Interfaces.External;

public interface ApiGenerationAbility
{
    public string Name { get; set; }
    public string url { get; set; }
}


public interface ApiAbility
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Is_main_series { get; set; }
    public ApiGenerationAbility MyProperty { get; set; }
}
