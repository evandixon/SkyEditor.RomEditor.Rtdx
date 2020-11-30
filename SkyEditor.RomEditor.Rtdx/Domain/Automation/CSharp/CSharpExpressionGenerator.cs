﻿using SkyEditor.RomEditor.Domain.Rtdx;
using SkyEditor.RomEditor.Domain.Rtdx.Constants;
using SkyEditor.RomEditor.Infrastructure;
using System;
using System.Collections.Generic;

namespace SkyEditor.RomEditor.Domain.Automation.CSharp
{
    public interface ICSharpExpressionGenerator : IScriptExpressionGenerator
    {
    }

    public class CreatureIndexCSharpExpressionGenerator : ICSharpExpressionGenerator
    {
        public CreatureIndexCSharpExpressionGenerator(ICommonStrings? commonStrings = null)
        {
            this.commonStrings = commonStrings;
        }

        private readonly ICommonStrings? commonStrings;

        public string Generate(object? obj)
        {
            if (!(obj is CreatureIndex index))
            {
                throw new ArgumentException("Unsupported value type");
            }

            string? friendlyName = commonStrings?.Pokemon?.GetValueOrDefault(index);
            if (!string.IsNullOrEmpty(friendlyName))
            {
                return $"CreatureIndex.{obj:f} /* {friendlyName} */";
            }
            else
            {
                return $"CreatureIndex.{obj:f}";
            }
        }
    }

    public class WazaIndexCSharpExpressionGenerator : ICSharpExpressionGenerator
    {
        public WazaIndexCSharpExpressionGenerator(ICommonStrings? commonStrings = null)
        {
            this.commonStrings = commonStrings;
        }

        private readonly ICommonStrings? commonStrings;

        public string Generate(object? obj)
        {
            if (!(obj is WazaIndex index))
            {
                throw new ArgumentException("Unsupported value type");
            }

            string? friendlyName = commonStrings?.Moves?.GetValueOrDefault(index);
            if (!string.IsNullOrEmpty(friendlyName))
            {
                return $"WazaIndex.{obj:f} /* {friendlyName} */";
            }
            else
            {
                return $"WazaIndex.{obj:f}";
            }
        }
    }

    public class DungeonIndexCSharpExpressionGenerator : ICSharpExpressionGenerator
    {
        public DungeonIndexCSharpExpressionGenerator(ICommonStrings? commonStrings = null)
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
                return $"DungeonIndex.{obj:f} /* {friendlyName} */";
            }
            else
            {
                return $"DungeonIndex.{obj:f}";
            }
        }
    }
}
