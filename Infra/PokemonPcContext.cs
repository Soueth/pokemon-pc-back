// using Microsoft.EntityFrameworkCore;
// using PokemonPc.Models;

// public class PokemonPcContext : DbContext
// {
//     public DbSet<Pokedex> Pokedex => Set<Pokedex>();
//     public DbSet<Pokemon> Pokemon => Set<Pokemon>();
//     public DbSet<Trainers> Trainers => Set<Trainers>();
//     public DbSet<Users> Users => Set<Users>();
//     public DbSet<Items> Items => Set<Items>();
//     public DbSet<ItemsBox> ItemsBox => Set<ItemsBox>();
//     public DbSet<Abilities> Abilities => Set<Abilities>();
//     public DbSet<AbilityPokemon> AbilityPokemon => Set<AbilityPokemon>();
//     public DbSet<Moves> Moves => Set<Moves>();
//     public DbSet<Teams> Teams => Set<Teams>();
//     public DbSet<MovesPokemon> MovesPokemon => Set<MovesPokemon>();

//     public PokemonPcContext(DbContextOptions<PokemonPcContext> options) : base(options) {}

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);
//     }
// }