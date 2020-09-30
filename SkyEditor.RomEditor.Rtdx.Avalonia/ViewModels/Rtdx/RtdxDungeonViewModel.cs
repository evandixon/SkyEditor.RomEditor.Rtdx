using SkyEditor.RomEditor.Domain.Rtdx;
using SkyEditor.RomEditor.Domain.Rtdx.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.RomEditor.Avalonia.ViewModels.Rtdx
{
    public class RtdxDungeonViewModel : ViewModelBase
    {
        public RtdxDungeonViewModel(IDungeonModel model, ICommonStrings commonStrings)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(model));
        }

        private readonly IDungeonModel model;

        public string Name => model.DungeonName;

        public void ReloadFromModel()
        {
        }
    }
}
