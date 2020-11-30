namespace SkyEditor.RomEditor.Domain.Automation.CSharp
{
    public interface ICSharpChangeScriptGenerator
    {
        string GenerateCSharpChangeScript(int indentLevel = 0);
    }
}
