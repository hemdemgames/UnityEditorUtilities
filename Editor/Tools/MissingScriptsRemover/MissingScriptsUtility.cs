using UnityEditor;
using UnityEngine;

namespace HemdemGames.EditorUtilities.Tools
{
    public class MissingScriptsUtility
    {
        public static int RemoveMissingScripts(GameObject[] gameObjects, bool includeChildren)
        {
            int missingScriptCount = 0;

            for (int i = 0; i < gameObjects.Length; i++)
            {
                missingScriptCount += RemoveMissingScripts(gameObjects[i], includeChildren);
            }
            
            return missingScriptCount;
        }

        public static int RemoveMissingScripts(GameObject gameObject, bool includeChildren)
        {
            int missingScriptCount = 0;

            if (gameObject.IsPrefabAsset())
            {
                Debug.LogError("remove missing scripts are not support in prefab. Usable only prefab edit mode");
            }
            else
            {
                missingScriptCount += UnityEditor.GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);

                if (includeChildren)
                {
                    missingScriptCount += RemoveMissingScriptsInChildren(gameObject);
                }

                SaveGameObject(gameObject);
            }

            return missingScriptCount;
        }

        private static void SaveGameObject(GameObject gameObject)
        {
            UnityEditor.EditorUtility.SetDirty(gameObject);
        }

        private static int RemoveMissingScriptsInChildren(GameObject gameObject)
        {
            int missingScriptCount = 0;
            
            int childCount = gameObject.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                missingScriptCount += RemoveMissingScripts(gameObject.transform.GetChild(i).gameObject, true);
            }

            return missingScriptCount;
        }
    }
}
