using SkyEditor.RomEditor.Domain.Rtdx.Constants;
using SkyEditor.RomEditor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyEditor.RomEditor.Domain.Rtdx.Models
{
    public interface IDungeonCollection
    {
        IDungeonModel[] Dungeons { get; }
        IDungeonModel? GetDungeonById(DungeonIndex id);
        void Flush();
    }

    public class DungeonCollection : IDungeonCollection
    {
        public DungeonCollection(IRtdxRom rom)
        {
            this.rom = rom ?? throw new ArgumentNullException(nameof(rom));

            this.Dungeons = LoadDungeons();
        }

        protected readonly IRtdxRom rom;

        public IDungeonModel[] Dungeons { get; }

        public IDungeonModel? GetDungeonById(DungeonIndex id)
        {
            return Dungeons.FirstOrDefault(s => s.Id == id);
        }

        private IDungeonModel[] LoadDungeons()
        {
            var commonStrings = rom.GetCommonStrings();
            var dungeonData = rom.GetDungeonDataInfo();
            var dungeonExtra = rom.GetDungeonExtra();
            var dungeonBalance = rom.GetDungeonBalance();

            var dungeons = new List<DungeonModel>();
            foreach (var dungeon in dungeonData.Entries)
            {
                dungeons.Add(new DungeonModel(commonStrings, dungeon.Value)
                {
                    Id = dungeon.Key,
                    Extra = dungeonExtra.Entries.GetValueOrDefault(dungeon.Key),
                    Balance = dungeonBalance.Entries[dungeon.Value.DungeonBalanceIndex]
                });
            }
            dungeons.Sort((d1, d2) => d1.Data.SortKey.CompareTo(d2.Data.SortKey));
            return dungeons.ToArray();
        }

        public void Flush()
        {
            var dungeonExtra = rom.GetDungeonExtra();
            var dungeonBalance = rom.GetDungeonBalance();
            foreach (var dungeon in Dungeons)
            {
                if (dungeon.Extra != null)
                {
                    dungeonExtra.Entries[dungeon.Id] = dungeon.Extra;
                }
                if (dungeon.Balance != null)
                {
                    dungeonBalance.Entries[dungeon.Data.DungeonBalanceIndex] = dungeon.Balance;
                }
            }
        }
    }
}
