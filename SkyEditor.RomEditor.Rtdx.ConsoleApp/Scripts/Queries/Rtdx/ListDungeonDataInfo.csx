﻿#load "../../../Stubs/RTDX.csx"

using System;
using System.Collections.Generic;
using SkyEditor.RomEditor.Domain.Rtdx.Constants;
using SkyEditor.RomEditor.Domain.Rtdx.Models;
using SkyEditor.RomEditor.Domain.Rtdx.Structures;
using DungeonFeature = SkyEditor.RomEditor.Domain.Rtdx.Structures.DungeonDataInfo.Entry.Feature;
using System.Linq;
using System.Runtime.CompilerServices;

var strings = Rom.GetCommonStrings();
var fixedPokemon = Rom.GetFixedPokemon().Entries;
var dungeons = Rom.GetDungeons().Dungeons;

// -- Formatters ---------------------------------

public string GetPokemonName(CreatureIndex id)
{
    return strings.Pokemon.ContainsKey(id) ? strings.Pokemon[id] : "(Unknown :" + (int)id + ")";
}

public string FormatFloor(IDungeonModel dungeon, int floor)
{
    string prefix = (dungeon.Data.Features.HasFlag(DungeonFeature.FloorDirection)) ? "" : "B";
    return prefix + floor + "F";
}

public string FormatFloors(IDungeonModel dungeon)
{
    if (dungeon.Extra == null) return "--";
    return FormatFloor(dungeon, dungeon.Extra.Floors);
}

public string FormatTeammates(int teammates)
{
    return (teammates > 1) ? "yes" : "no";
}

public string FormatItems(int items)
{
    return (items > 0) ? "yes" : "no";
}

public string FormatFeature(DungeonFeature features, DungeonFeature feature)
{
    return (features.HasFlag(feature)) ? "yes" : "no";
}

public string FormatFeatures(DungeonFeature features)
{
    var featureNames = new List<string>();
    if (!features.HasFlag(DungeonFeature.AutoRevive)) featureNames.Add("Auto-revive");
    if (features.HasFlag(DungeonFeature.Scanning)) featureNames.Add("Scanning");
    if (features.HasFlag(DungeonFeature.Radar)) featureNames.Add("Radar");
    return string.Join(", ", featureNames);
}

public string FormatFeaturesBits(DungeonFeature features)
{
    var binary = Convert.ToString((int)features, 2);
    return binary.Replace('0', '-').Replace('1', '#').PadLeft(19, '-');
}

// -----------------------------------------------

//Console.WriteLine("#    Index   Dungeon                    Floors");
Console.WriteLine("#    Index   Dungeon                    Floors   Teammates   Items   Level reset   Recruitable   Features                       210FEDCBA9876543210   0x08   0x0A   d_balance   0x13   0x17   0x18   0x19");
foreach (var dungeon in dungeons)
{
    if (dungeon.Id == default)
    {
        continue;
    }

    var data = dungeon.Data;
    var extra = dungeon.Extra;
    var balance = dungeon.Balance;
    var floorInfos = balance.FloorInfos;
    var wildPokemon = balance.WildPokemon;
    var data3 = balance.Data3;
    var data4 = balance.Data4;

    // Print basic dungeon info
    //Console.Write($"{dungeon.Id,-4} {data.Index,5}   {dungeon.DungeonName,-28} {FormatFloors(dungeon),4}");

    // Print complete dungeon info
    Console.WriteLine($"{(int)dungeon.Id,-4} {data.Index,5}   {dungeon.DungeonName,-28} {FormatFloors(dungeon),4}      "
        + $"{FormatTeammates(data.MaxTeammates),3}       "
        + $"{FormatItems(data.MaxItems),3}        "
        + $"{FormatFeature(data.Features, DungeonFeature.LevelReset),3}           "
        + $"{FormatFeature(data.Features, DungeonFeature.WildPokemonRecruitable),3}       "
        + $"{FormatFeatures(data.Features),-30} "
        + $"{FormatFeaturesBits(data.Features)}    "
        + $"{data.Short08,3}    {data.Short0A,3}      {data.DungeonBalanceIndex,3}       {data.Byte13,3}    {data.Byte17,3}    {data.Byte18,3}    {data.Byte19,3}");

    // Print floor infos
    /*foreach (var info in floorInfos)
    {
        Console.WriteLine($"   {info.Index,5}  "
            + $"{info.InvitationIndex,5}  "
            + $"{info.Short02,5}  "
            + $"{info.Short24,5}  "
            + $"{info.Short26,5}  "
            + $"{info.Short28,5}  "
            + $"{info.Short2A,5}  "
            + $"{info.Byte2C,3}  "
            + $"{info.Byte2D,3}  "
            + $"{info.Byte2E,3}  "
            + $"{info.Byte2F,3}  "
            + $"{info.Short30,5}  "
            + $"{info.Short32,5}  "
            + $"{info.Byte34,3}  "
            + $"{info.Byte35,3}  "
            + $"{info.Byte36,3}  "
            + $"{info.Byte36,3}  "
            + $"{string.Join(",", info.Bytes37to53)}  "
            + $"{string.Join(",", info.Bytes55to61)}");
    }*/

    // Print fainted Pokemon
    /*var faintedPokemon = (from pokemon in fixedPokemon
                          where (int)pokemon.DungeonIndex == data.Index
                          select GetPokemonName(pokemon.PokemonId)).ToList();
    if (faintedPokemon.Count > 0)
    {
        Console.WriteLine($"  Fainted Pokemon: {string.Join(", ", faintedPokemon)}");
    }*/

    // Print Pokemon in Mystery Houses
    /*int prevInvitationIndex = -1;
    var mysteryHousePokemon = new List<string>();
    foreach (var info in from info in floorInfos
                         where info.InvitationIndex != 0 && prevInvitationIndex != info.InvitationIndex
                         select info)
    {
        prevInvitationIndex = info.InvitationIndex;
        mysteryHousePokemon.AddRange(from pokemon in fixedPokemon
                                     where pokemon.InvitationIndex == info.InvitationIndex
                                     select GetPokemonName(pokemon.PokemonId));
    }

    if (mysteryHousePokemon.Count > 0)
    {
        Console.WriteLine($"  Mystery House Pokemon: {string.Join(", ", mysteryHousePokemon)}");
    }*/

    // Print wild Pokemon
    /*if (wildPokemon != null)
    {
        Console.WriteLine("      #   Pokemon         Lvl    HP   Atk   Def   SpA   SpD   Spe    XP Yield");
        Console.WriteLine("             Spawn  Recruit");
        Console.WriteLine("      Floor   rate   level   0x0B");
        var stats = wildPokemon.Stats;
        var floors = wildPokemon.Floors;

        foreach (var stat in stats)
        {
            var index = stat.Index + 1;
            var name = GetPokemonName(stat.CreatureIndex);
            if (stat.XPYield != 0 || stat.HitPoints != 0 || stat.Attack != 0 || stat.Defense != 0 ||
                stat.SpecialAttack != 0 || stat.SpecialDefense != 0 || stat.Speed != 0 || stat.Level != 0)
            {
                var strongFoe = (stat.StrongFoe != 0) ? "Strong Foe" : "";
                Console.WriteLine($"   {index,4}   "
                    + $"{name,-14}  "
                    + $"{stat.Level,3}   "
                    + $"{stat.HitPoints,3}   "
                    + $"{stat.Attack,3}   "
                    + $"{stat.Defense,3}   "
                    + $"{stat.SpecialAttack,3}   "
                    + $"{stat.SpecialDefense,3}   "
                    + $"{stat.Speed,3}    "
                    + $"{stat.XPYield,8}   "
                    + $"{strongFoe}");

                for (int k = 0; k < dungeon.Extra.Floors; k++)
                {
                    var floor = floors[k].Entries[stat.Index];
                    if (floor.SpawnRate != 0)
                    {
                        Console.WriteLine($"       {FormatFloor(dungeon, k + 1),4}    {floor.SpawnRate,3}    {floor.RecruitmentLevel,3}    {floor.Byte0B,3}");
                    }
                }
            }
        }
    }*/

    // Print unknown data from the third SIR0 file in dungeon_balance.bin
    /*if (data3 != null)
    {
        var records = data3.Records;
        int prevIndex = -1;
        var prevShort02s = new List<short>();
        var len = dungeon.Extra.Floors; // records.Length - 1
        for (int j = 0; j < len; j++)
        {
            var record = records[j];
            var short02s = new List<short>();
            
            foreach (var entry in record.Entries)
            {
                short02s.Add(entry.Short02);
            }

            if (prevShort02s.Count == 0 || !prevShort02s.SequenceEqual(short02s))
            {
                prevShort02s = short02s;
                if (prevIndex != -1 && prevIndex != j)
                {
                    Console.WriteLine($"..{FormatFloor(dungeon, len)}: *");
                }
                Console.WriteLine($"  {FormatFloor(dungeon, j + 1)}: {string.Join("  ", short02s)}");
                prevIndex = j + 1;
            }

        }
        if (prevIndex != -1 && prevIndex != len) {
            Console.WriteLine($"..{FormatFloor(dungeon, len)}: *");
        }
    }*/

    // Print unknown (and mostly uninteresting) data from the fourth SIR0 file in dungeon_balance.bin
    /*if (data4 != null)
    {
        for (int i = 0; i < data4.Records.Length; i++)
        {
            DungeonBalance.DungeonBalanceDataEntry4.Record record = data4.Records[i];
            var short00s = new List<short>();
            var short02s = new List<short>();
            var int04s = new List<int>();

            foreach (var entry in record.Entries)
            {
                short00s.Add(entry.Short00);
                short02s.Add(entry.Short02);
                int04s.Add(entry.Int04);
            }

            Console.WriteLine($"  {FormatFloor(dungeon, i + 1)}: {string.Join("  ", short00s)}");
            Console.WriteLine($"     {string.Join("  ", short02s)}");
            Console.WriteLine($"     {string.Join("  ", int04s)}");
        }
    }*/
}
