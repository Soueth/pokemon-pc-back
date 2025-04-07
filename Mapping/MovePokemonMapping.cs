using MongoDB.Bson;
using PokemonPc.Interfaces.External;
using PokemonPc.Models;
using PokemonPc.Utils.Exceptions;
using PokemonPc.Utils.Types;

namespace PokemonPc.Mapping;

public static class MovePokemonMapping
{
    public static MovePokemon ToMovePokemon(this ApiPokemonMove apiMovePokemon, Move move, Entry entry)
    {
        if (!move.Id.HasValue) {
            throw new EmptyIdException(nameof(move.Id));
        }

        if (!entry.Id.HasValue) {
            throw new EmptyIdException(nameof(entry.Id));
        }

        VersionGroupDetails[] versions = apiMovePokemon.Version_group_details
            .Where((v) => v.Version_group.Name == "scarlet-violet")
            .ToArray();

            // .<VersionGroupDetails>((v) => v.Version_group.Name == "scarlet-violet");

        if (versions.Length == 0) {
            throw new NotFoundElementException(nameof(VersionGroupDetails), "scarlet-violet");
        }

        LearningMethod[] learningMethods = [];
        int? learningLevel = null;

        foreach (VersionGroupDetails version in versions)
        {
            learningMethods.Append
            (
                version.Move_learn_method.Name switch
                {
                    "level-up" => LearningMethod.Level,
                    "machine" => LearningMethod.TM,
                    "tutor" => LearningMethod.Tutor,
                    _ => LearningMethod.Other,
                }
            );

            if (version.Level_learned_at != null)
            {
                learningLevel = version.Level_learned_at;
            }
        }

        return new()
        {
            MoveId = move.Id.Value,
            PokemonId = entry.Id.Value,
            LearningMethod = learningMethods,
            LearningLevel = learningLevel,
        };
    }
}
