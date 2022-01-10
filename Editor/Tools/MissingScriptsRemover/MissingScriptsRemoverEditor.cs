using HemdemGames.EditorUtilities.Tools.MissingScriptsRemover;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace HemdemGames.EditorUtilities.Tools
{
    internal class MissingScriptsRemoverEditor : Editor
    {
        [MenuItem("GameObject/Editor Toolkit/Remove Missing Scripts/", priority = 1)]
        static void Init() { }

        private static bool HasSelectedObjects()
        {
            return Selection.gameObjects.Length > 0;
        }

        [MenuItem("GameObject/Editor Toolkit/Remove Missing Scripts/Selections", true)]
        static bool RemoveInSelectedsValidate()
        {
            return HasSelectedObjects();
        }

        [MenuItem("GameObject/Editor Toolkit/Remove Missing Scripts/Selections")]
        static void RemoveInSelecteds()
        {
            MissingScriptsEditorUtility.RemoveMissingScripts(Selection.gameObjects);
        }

        [MenuItem("GameObject/Editor Toolkit/Remove Missing Scripts/In Prefab", true)]
        static bool RemoveInOpenedPrefabValidate()
        {
            return IsPrefabOpen();
        }

        private static bool IsPrefabOpen()
        {
            return PrefabStageUtility.GetCurrentPrefabStage() != null;
        }

        [MenuItem("GameObject/Editor Toolkit/Remove Missing Scripts/In Prefab")]
        static void RemoveInOpenedPrefab()
        {
            MissingScriptsEditorUtility.RemoveMissingScripts(PrefabStageUtility.GetCurrentPrefabStage().prefabContentsRoot);
        }

        [MenuItem("GameObject/Editor Toolkit/Remove Missing Scripts/In Active Scene")]
        static void RemoveInActiveScene()
        {
            var sceneObjects = EditorSceneManager.GetActiveScene().GetRootGameObjects();
            MissingScriptsEditorUtility.RemoveMissingScripts(sceneObjects);
        }
    }
}
