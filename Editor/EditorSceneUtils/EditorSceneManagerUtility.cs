using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace HemdemGames.EditorUtilities.EditorSceneUtils
{
    internal static class EditorSceneManagerUtility
    {
        public static Scene[] GetLoadedScenes()
        {
            Scene[] loadedScenes = new Scene[EditorSceneManager.loadedSceneCount];

            for (int i = 0; i < loadedScenes.Length; i++)
            {
                loadedScenes[i] = EditorSceneManager.GetSceneAt(i);
            }

            return loadedScenes;
        }

        public static void SaveLoadedScenesIfUserWantsTo()
        {
            EditorSceneManager.SaveModifiedScenesIfUserWantsTo(GetLoadedScenes());
        }
    }
}