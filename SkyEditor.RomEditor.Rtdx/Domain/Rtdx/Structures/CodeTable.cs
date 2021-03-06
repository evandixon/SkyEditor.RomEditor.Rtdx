using System;
using System.Collections.Generic;
using SkyEditor.RomEditor.Domain.Common.Structures;
using SkyEditor.RomEditor.Domain.Rtdx.Constants;

namespace SkyEditor.RomEditor.Domain.Rtdx.Structures
{
    public class RtdxCodeTable : CodeTable
    {
        private static readonly Dictionary<string, Type> StaticConstantReplacementTable = new Dictionary<string, Type>()
        {
            { "kind_p:", typeof(CreatureIndex) },
            { "item:", typeof(ItemIndex) },
            { "dungeon:", typeof(DungeonIndex) },
            { "ability:", typeof(AbilityIndex) },
            { "waza:", typeof(WazaIndex) },
        };

        protected override Dictionary<string, Type> ConstantReplacementTable => StaticConstantReplacementTable;

        public RtdxCodeTable()
        {
        }

        public RtdxCodeTable(byte[] data) : base(data)
        {
        }
    }
}
