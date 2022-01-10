using UnityEngine;
using System.Collections;
using UnityEditor;

namespace HemdemGames.EditorUtilities.Tools
{
    internal static class EditorUtility
    {
        public static void FocusSceneObject(GameObject sceneObject)
        {
            var bounds = BoundsUtility.CalculateBounds(sceneObject);
            SceneView.lastActiveSceneView.Frame(bounds, false);
            EditorGUIUtility.PingObject(sceneObject);
        }
    }
}