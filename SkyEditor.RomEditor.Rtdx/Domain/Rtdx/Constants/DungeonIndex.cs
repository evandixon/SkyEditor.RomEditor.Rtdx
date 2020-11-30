﻿using SkyEditor.RomEditor.Domain.Automation.CSharp;
using SkyEditor.RomEditor.Domain.Automation.Lua;
using SkyEditor.RomEditor.Infrastructure;
using System;
using System.Collections.Generic;

namespace SkyEditor.RomEditor.Domain.Rtdx.Constants
{
    public class DungeonIndexLuaExpressionGenerator : ILuaExpressionGenerator
    {
        public DungeonIndexLuaExpressionGenerator(ICommonStrings? commonStrings = null)
        {
            this.commonStrings = commonStrings;
        }

        private readonly ICommonStrings? commonStrings;

        public string Generate(object? obj)
        {
            if (!(obj is DungeonIndex index))
            {
                throw new ArgumentException("Unsupported value type");
            }

            string? friendlyName = commonStrings?.Dungeons?.GetValueOrDefault(index);
            if (!string.IsNullOrEmpty(friendlyName))
            {
                return $"Const.dungeon.Index.{obj:f} --[[{friendlyName}]]";
            }
            else
            {
                return $"Const.dungeon.Index.{obj:f}";
            }
        }
    }
    [LuaExpressionGenerator(typeof(DungeonIndexLuaExpressionGenerator))]
    [CSharpExpressionGenerator(typeof(DungeonIndexCSharpExpressionGenerator))]
    public enum DungeonIndex
    {
		NONE = 0,
		D001 = 1,
		D002 = 2,
		D003 = 3,
		D004 = 4,
		D005 = 5,
		D006 = 6,
		D007 = 7,
		D008 = 8,
		D009 = 9,
		D010 = 10,
		D011 = 11,
		D012 = 12,
		D013 = 13,
		D014 = 14,
		D015 = 15,
		D016 = 16,
		D017 = 17,
		D018 = 18,
		D019 = 19,
		D020 = 20,
		D021 = 21,
		D022 = 22,
		D023 = 23,
		D024 = 24,
		D025 = 25,
		D026 = 26,
		D027 = 27,
		D028 = 28,
		D029 = 29,
		D030 = 30,
		D031 = 31,
		D032 = 32,
		D033 = 33,
		D034 = 34,
		D035 = 35,
		D036 = 36,
		D037 = 37,
		D038 = 38,
		D039 = 39,
		D040 = 40,
		D041 = 41,
		D042 = 42,
		D043 = 43,
		D044 = 44,
		D045 = 45,
		D046 = 46,
		D047 = 47,
		D048 = 48,
		D049 = 49,
		D050 = 50,
		D051 = 51,
		D052 = 52,
		D053 = 53,
		D054 = 54,
		D055 = 55,
		D056 = 56,
		D057 = 57,
		D058 = 58,
		D059 = 59,
		D060 = 60,
		D061 = 61,
		D062 = 62,
		D063 = 63,
		D064 = 64,
		D065 = 65,
		D066 = 66,
		D067 = 67,
		D068 = 68,
		D069 = 69,
		D070 = 70,
		D071 = 71,
		D072 = 72,
		D073 = 73,
		D074 = 74,
		D075 = 75,
		D076 = 76,
		D077 = 77,
		D078 = 78,
		D079 = 79,
		D080 = 80,
		D081 = 81,
		D082 = 82,
		D083 = 83,
		D084 = 84,
		D085 = 85,
		D086 = 86,
		D087 = 87,
		D088 = 88,
		D089 = 89,
		D090 = 90,
		D091 = 91,
		D092 = 92,
		D093 = 93,
		D094 = 94,
		D095 = 95,
		D096 = 96,
		D097 = 97,
		D098 = 98,
		D099 = 99,
		D100 = 100,
		QUEST_PRIZE = 101,
		QUEST_PRIZE_SYMBOL = 102,
		TOWN_SHOP = 103,
		FIXEDMAP_CHEST = 104,
		FRIEND_RESCUE_PRIZE = 105,
		FRIEND_RESCUE_PRIZE_SPECIAL = 106,
		FRIEND_RESCUE_PRIZE_DELUXE = 107,
		EVENT_PRIZE = 108,
		TOWN_SHOP_AZUKI = 109,
		END = 110
	}
}
