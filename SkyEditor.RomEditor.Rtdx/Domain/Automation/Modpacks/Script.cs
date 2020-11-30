﻿namespace SkyEditor.RomEditor.Domain.Automation.Modpacks
{
    public struct Script
    {
        public Script(ScriptType type, string script)
        {
            this.Type = type;
            this.Value = script;
        }

        public ScriptType Type { get; }
        public string Value { get; }
    }
}
