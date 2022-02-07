using UnityEngine;

namespace HemdemGames.EditorUtilities.EditorSceneUtils
{
    internal static class EditorSceneSnapshotManager
    {
        private static EditorSceneSnapshot lastSnapshot;

        internal static void CaptureSnapshot()
        {
            lastSnapshot = EditorSceneSnapshotCapturer.CaptureSnapshot();
        }

        internal static void LoadLastSnapshot() => EditorSceneSnapshotLoader.LoadSnapshot(lastSnapshot);
    }
}