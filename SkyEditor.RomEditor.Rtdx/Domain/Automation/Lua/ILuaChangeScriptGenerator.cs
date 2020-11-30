namespace SkyEditor.RomEditor.Domain.Automation.Lua
{
    public interface ILuaChangeScriptGenerator
    {
        string GenerateLuaChangeScript(int indentLevel = 0);
    }
}
