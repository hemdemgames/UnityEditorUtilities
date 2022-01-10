using UnityEditor;
using UnityEngine;

namespace HemdemGames.EditorUtilities.Tools.MissingScriptsRemover
{
    public class MissingScriptsEditorUtility
    {
        internal static void RemoveMissingScripts(GameObject gameObject)
        {
            RemoveMissingScripts(new GameObject[] { gameObject });
        }

        internal static void RemoveMissingScripts(GameObject[] gameObjects)
        {
            Undo.RecordObjects(gameObjects, "Missing Scripts Tool");
            int missingScriptCount = MissingScriptsUtility.RemoveMissingScripts(gameObjects, true);
            Debug.Log($"Removed {missingScriptCount} Missing Scripts");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
