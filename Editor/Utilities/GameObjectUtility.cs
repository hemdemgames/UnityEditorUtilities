using UnityEngine;
using System.Collections;
using UnityEditor;

namespace HemdemGames.EditorUtilities.Tools
{
    internal static class GameObjectUtility
    {
        public static bool IsInScene(this GameObject gameObject)
        {
            return gameObject.scene != null &&
                gameObject.scene.name != null &&
                gameObject.scene.path != null;
        }

        public static bool IsPrefabAsset(this GameObject gameObject)
        {
            bool isNotInstance = UnityEditor.PrefabUtility.GetPrefabInstanceStatus(gameObject) == PrefabInstanceStatus.NotAPrefab;
            return gameObject.IsPrefab() && isNotInstance;
        }

        public static bool IsPrefab(this GameObject gameObject)
        {
            return UnityEditor.PrefabUtility.GetPrefabAssetType(gameObject) != PrefabAssetType.NotAPrefab;
        }

        public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation)
        {
            var instance = Instantiate(original);
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            return instance;
        }

        public static GameObject Instantiate(GameObject original)
        {
            if (original.IsPrefab())
            {
                if (original.IsPrefabAsset())
                {
                    return UnityEditor.PrefabUtility.InstantiatePrefab(original) as GameObject;
                }
                else
                {
                    return PrefabUtility.InstantiatePrefabInstance(original);
                }
            }
            else
            {
                return GameObject.Instantiate(original);
            }
        }
    }
}