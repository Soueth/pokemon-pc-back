using System;

namespace PokemonPc.Models;

public class Users : BaseModel
{
    public string Email { get; set; } = null!;

    public ICollection<Trainers> Trainer { get; set; } = null!;

    // TODO: o atributo 'password' e a lógica de autentificação
}
