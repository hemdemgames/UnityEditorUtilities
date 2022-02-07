using System.Linq;
using UnityEditor.SceneManagement;

namespace HemdemGames.EditorUtilities.EditorSceneUtils
{
    internal static class EditorSceneSnapshotCapturer
    {
        internal static EditorSceneSnapshot CaptureSnapshot()
        {
            return new EditorSceneSnapshot()
            {
                activeScenePath = EditorSceneManager.GetActiveScene().path,
                loadedScenePaths = EditorSceneManagerUtility.GetLoadedScenes().Select(scene => scene.path).ToList()
            };
        }
    }
}