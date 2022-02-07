using UnityEditor;
using UnityEngine;

namespace HemdemGames.EditorUtilities.PlayModeComponentSave
{
    public class ComponentEditor
    {
        [MenuItem("CONTEXT/Component/Save Values For Edit Mode", true)]
        static bool SaveValidFunc()
        {
            return Application.isPlaying;
        }

        [MenuItem("CONTEXT/Component/Save Values For Edit Mode")]
        static void Save(MenuCommand command)
        {
            ComponentSaveManager.SaveComponent(command.context as Component);
        }

        [MenuItem("CONTEXT/Component/Delete Saved Values", true)]
        static bool DeleteValidFunc(MenuCommand command)
        {
            return Application.isPlaying && ComponentSaveManager.IsComponentSaved(command.context as Component);
        }
        
        [MenuItem("CONTEXT/Component/Delete Saved Values")]
        static void Delete(MenuCommand command)
        {
            ComponentSaveManager.DeleteComponent(command.context as Component);
        }
    }
}