﻿using SkyEditor.RomEditor.Domain.Rtdx.Constants;
using SkyEditor.RomEditor.Domain.Rtdx.Structures;
using SkyEditor.RomEditor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyEditor.RomEditor.Domain.Rtdx
{
    public interface ICommonStrings
    {
        Dictionary<CreatureIndex, string> Pokemon { get; }
        Dictionary<WazaIndex, string> Moves { get; }
        Dictionary<DungeonIndex, string> Dungeons { get; }

        /// <summary>
        /// Gets the name of a Pokemon by the internal Japanese name.
        /// </summary>
        /// <param name="internalName">Internal Japanese name such as "FUSHIGIDANE"</param>
        /// <returns>User-facing name such as "Bulbasaur", or null if the internal name could not be found</returns>
        string? GetPokemonNameByInternalName(string internalName);

        string? GetPokemonTaxonomy(int speciesId);

        /// <summary>
        /// Gets the name of a move by the internal Japanese name.
        /// </summary>
        string? GetMoveNameByInternalName(string internalName);

        string? GetMoveName(WazaIndex wazaIndex);

        string? GetAbilityNameByInternalName(string internalName);

        string? GetAbilityName(AbilityIndex abilityIndex);

        string? GetDungeonNameByInternalName(string internalName);

        string? GetDungeonName(DungeonIndex dungeonIndex);
    }

    public class CommonStrings : ICommonStrings
    {
        private static readonly Dictionary<string, int> TextIdValues = Enum.GetValues(typeof(TextIDHash)).Cast<TextIDHash>().ToDictionary(h => h.ToString("f"), h => (int)h);

        public CommonStrings(MessageBinEntry common)
        {
            this.common = common ?? throw new ArgumentNullException(nameof(common));

            Pokemon = new Dictionary<CreatureIndex, string>();
            var creatures = Enum.GetValues(typeof(CreatureIndex)).Cast<CreatureIndex>().ToArray();
            foreach (CreatureIndex creature in creatures)
            {
                if (creature == default)
                {
                    continue;
                }

                var name = GetPokemonNameByInternalName(creature.ToString("f"));
                Pokemon.Add(creature, name ?? "");
            }

            Moves = new Dictionary<WazaIndex, string>();
            var moves = Enum.GetValues(typeof(WazaIndex)).Cast<WazaIndex>().ToArray();
            foreach (WazaIndex waza in moves)
            {
                if (waza == default)
                {
                    continue;
                }

                var nameHash = TextIdValues.GetValueOrDefault("WAZA_NAME__WAZA_" + waza.ToString("f"));
                var name = common.GetStringByHash(nameHash);
                Moves.Add(waza, name ?? "");
            }

            Dungeons = new Dictionary<DungeonIndex, string>();
            var dungeons = Enum.GetValues(typeof(DungeonIndex)).Cast<DungeonIndex>().ToArray();
            foreach (DungeonIndex dungeon in dungeons)
            {
                if (dungeon == default)
                {
                    continue;
                }

                var name = GetDungeonNameByInternalName(dungeon.ToString("f"));
                Dungeons.Add(dungeon, name ?? "");
            }
        }

        private readonly MessageBinEntry common;

        public Dictionary<CreatureIndex, string> Pokemon { get; }
        public Dictionary<WazaIndex, string> Moves { get; }
        public Dictionary<DungeonIndex, string> Dungeons { get; }

        /// <summary>
        /// Gets the name of a Pokemon by the internal Japanese name.
        /// </summary>
        /// <param name="internalName">Internal Japanese name such as "FUSHIGIDANE"</param>
        /// <returns>User-facing name such as "Bulbasaur", or null if the internal name could not be found</returns>
        public string? GetPokemonNameByInternalName(string internalName)
        {
            var nameHash = TextIdValues.GetValueOrDefault("POKEMON_NAME__POKEMON_" + internalName.ToUpper());
            return common.GetStringByHash(nameHash);
        }

        public string? GetPokemonTaxonomy(int taxonId)
        {
            taxonId -= 1; // It's stored in pokemon_data_info 1 higher than the internal id
            var nameHash = TextIdValues.GetValueOrDefault("POKEMON_TAXIS__SPECIES_" + taxonId.ToString().PadLeft(3, '0'));
            return common.GetStringByHash(nameHash);
        }

        /// <summary>
        /// Gets the name of a move by the internal Japanese name.
        /// </summary>
        public string? GetMoveNameByInternalName(string internalName)
        {
            var nameHash = TextIdValues.GetValueOrDefault("WAZA_NAME__WAZA_" + internalName.ToUpper());
            return common.GetStringByHash(nameHash);
        }

        public string? GetMoveName(WazaIndex wazaIndex)
        {
            return GetMoveNameByInternalName(wazaIndex.ToString("f"));
        }

        public string? GetAbilityNameByInternalName(string internalName)
        {
            var nameHash = TextIdValues.GetValueOrDefault("ABILITY_NAME__" + internalName.ToUpper());
            return common.GetStringByHash(nameHash);
        }

        public string? GetAbilityName(AbilityIndex abilityIndex)
        {
            return GetAbilityNameByInternalName(abilityIndex.ToString("f"));
        }

        public string? GetDungeonNameByInternalName(string internalName)
        {
            var nameHash = TextIdValues.GetValueOrDefault("DUNGEON_NAME__DUNGEON_" + internalName.ToUpper());
            return common.GetStringByHash(nameHash);
        }

        public string? GetDungeonName(DungeonIndex dungeonIndex)
        {
            return GetDungeonNameByInternalName(dungeonIndex.ToString("f"));
        }
    }
}
