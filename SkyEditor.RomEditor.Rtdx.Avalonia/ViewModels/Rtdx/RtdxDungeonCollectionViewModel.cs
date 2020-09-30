using ReactiveUI;
using SkyEditor.RomEditor.Domain.Rtdx;
using SkyEditor.RomEditor.Domain.Rtdx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyEditor.RomEditor.Avalonia.ViewModels.Rtdx
{
    public class RtdxDungeonCollectionViewModel : ViewModelBase
    {
        public RtdxDungeonCollectionViewModel(IDungeonCollection starterCollection, ICommonStrings commonStrings)
        {
            if (starterCollection == null)
            {
                throw new ArgumentNullException(nameof(starterCollection));
            }

            Dungeons = starterCollection.Dungeons.Select(s => new RtdxDungeonViewModel(s, commonStrings)).ToList();
            _selectedDungeon = Dungeons.First();
        }

        public IReadOnlyList<RtdxDungeonViewModel> Dungeons { get; }

        public RtdxDungeonViewModel SelectedDungeon
        {
            get => _selectedDungeon;
            set
            {
                _selectedDungeon = value;
                this.RaisePropertyChanged(nameof(SelectedDungeon));
            }
        }
        private RtdxDungeonViewModel _selectedDungeon;

        /// <summary>
        /// Signals that properties on the model were changed and the view model should emit property changed events where appropriate
        /// </summary>
        public void ReloadFromModel()
        {
            foreach (var dungeon in Dungeons)
            {
                dungeon.ReloadFromModel();
            }
            SelectedDungeon.ReloadFromModel();
        }
    }
}
