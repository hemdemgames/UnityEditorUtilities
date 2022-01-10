using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace HemdemGames.EditorUtilities.Tools
{
    internal class GameObjectReplace : EditorWindow
    {
        private GameObject replaceWith;
        private bool keepRotation;
        private GameObject[] selectedObjects;

        [MenuItem("GameObject/Editor Toolkit/Replace", priority = 0)]
        static void Open()
        {
            var window = GetWindow<GameObjectReplace>(true, "Replace Tool");
            var size = new Vector2(400, 70);
            window.minSize = size;
            window.maxSize = size;
        }

        [MenuItem("GameObject/Editor Toolkit/Replace", true)]
        static bool OpenValidate()
        {
            return Selection.gameObjects.Length > 0;
        }

        private void OnEnable()
        {
            selectedObjects = Selection.gameObjects;
        }

        private void OnGUI()
        {
            replaceWith = (GameObject)EditorGUILayout.ObjectField("Replace With", replaceWith, typeof(GameObject), true);
            keepRotation = EditorGUILayout.Toggle("Keep Rotation", keepRotation);

            if (GUILayout.Button("Replace"))
            {
                Replace();
            }
        }

        private void Replace()
        {
            InstantiateNewObjects();
            DestroyOldObjects();
            Close(); // Close Window
        }

        private void InstantiateNewObjects()
        {
            foreach (var selection in selectedObjects)
            {
                InstantiateNewObject(replaceWith, selection, keepRotation);
            }
        }

        private void DestroyOldObjects()
        {
            foreach (var selection in selectedObjects)
            {
                Undo.DestroyObjectImmediate(selection);
                DestroyImmediate(selection);
            }
        }

        private void InstantiateNewObject(GameObject newGameObject, GameObject oldGameObject, bool keepRotation)
        {
            Vector3 position = oldGameObject.transform.position;
            Quaternion rotation = keepRotation ? newGameObject.transform.rotation : oldGameObject.transform.rotation;
            GameObject instance = GameObjectUtility.Instantiate(newGameObject, position, rotation);
            instance.transform.SetParent(oldGameObject.transform.parent);
            Undo.RegisterCreatedObjectUndo(instance, "Replace");
        }
    }
}