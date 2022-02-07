using UnityEditor.SceneManagement;

namespace HemdemGames.EditorUtilities.EditorSceneUtils
{
    internal static class EditorSceneSnapshotLoader
    {
        internal static void LoadSnapshot(EditorSceneSnapshot snapshot)
        {
            EditorSceneManager.OpenScene(snapshot.loadedScenePaths[0], OpenSceneMode.Single);

            for (int i = 1; i < snapshot.loadedScenePaths.Count; i++)
            {
                EditorSceneManager.OpenScene(snapshot.loadedScenePaths[i], OpenSceneMode.Additive);
            }

            EditorSceneManager.SetActiveScene(EditorSceneManager.GetSceneByPath(snapshot.activeScenePath));
        }
    }
}