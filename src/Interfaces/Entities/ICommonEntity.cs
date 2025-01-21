using PokemonPc.Utils.Types;

public interface ICommonEntity
{
    public MongoId Id { get; set; }
    public DateTime? createdAt { get; set; }
    public DateTime? updatedAt { get; set; }
}