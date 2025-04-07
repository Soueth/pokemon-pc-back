using MongoDB.Bson.Serialization.Attributes;

namespace PokemonPc.Models;

public class Entry : Model
{
    [BsonElement("externalId")]
    public int ExternalId { get; set; }

    [BsonElement("number")]
    public int Number { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("types")]
    public string[] Types { get; set; } = [];

    [BsonElement("imageUrl")]
    public string ImageUrl { get; set; } = null!;
    
    [BsonElement("imageUrlFemale")]
    public string ImageUrlFemale { get; set; } = null!;

    [BsonElement("imageUrlShiny")]
    public string ImageUrlShiny { get; set; } = null!;

    [BsonElement("imageUrlShinyFemale")]
    public string ImageUrlShinyFemale { get; set; } = null!;
}