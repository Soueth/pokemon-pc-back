using PokemonPc.Utils.Types;

public interface IEntity
{
    public MongoId Id { get; set; }
    public DateTime? createdAt { get; set; }
    public DateTime? updatedAt { get; set; }
}