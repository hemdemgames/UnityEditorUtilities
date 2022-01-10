using UnityEngine;
using System.Collections;
using UnityEditor;

namespace HemdemGames.EditorUtilities.Tools
{
    internal static class PrefabUtility
    {
        public static GameObject InstantiatePrefabInstance(GameObject original)
        {
            var prefabSource = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(original);
            var instanceModifications = UnityEditor.PrefabUtility.GetPropertyModifications(original);
            var instance = UnityEditor.PrefabUtility.InstantiatePrefab(prefabSource) as GameObject;
            UnityEditor.PrefabUtility.SetPropertyModifications(instance, instanceModifications);
            return instance;
        }
    }
}