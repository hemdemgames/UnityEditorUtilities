
namespace HemdemGames.EditorUtilities.Tools.SelectionEditor
{
    internal interface ICustomTool
    {
        void DrawGUI();
        void OnActivated();
        void OnDeactivated();
        bool GUIButton();
    }
}