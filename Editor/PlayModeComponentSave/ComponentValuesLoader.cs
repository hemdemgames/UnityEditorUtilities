using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HemdemGames.EditorUtilities.PlayModeComponentSave
{
    internal static class ComponentValuesLoader
    {
        public static event Action OnLoadingFinish;

        private static ComponentData[] componentDatas;
        private static IEnumerator<string> scenePaths;
        private static bool isProcessing;

        public static void LoadComponentValues(IEnumerable<ComponentData> componentDataArray)
        {
            if (Application.isPlaying)
            {
                Debug.LogError("component values loader run in only edit mode");
                return;
            }

            if (isProcessing)
            {
                Debug.LogError("component values loader is busy");
                return;
            }

            componentDatas = componentDataArray.ToArray();
            scenePaths = componentDatas.Select(component => component.scenePath).Distinct().GetEnumerator();
            
            LoadNextSceneComponentValues();
        }

        private static void LoadNextSceneComponentValues()
        {
            bool hasNextScene = scenePaths.MoveNext();
            if (!hasNextScene)
            {
                AllComponentValuesLoaded();
                return;
            }

            isProcessing = true;
            
            EditorSceneManager.sceneOpened -= OnSceneOpened;
            EditorSceneManager.sceneOpened += OnSceneOpened;

            EditorSceneManager.OpenScene(scenePaths.Current);
        }

        private static void AllComponentValuesLoaded()
        {
            componentDatas = null;
            scenePaths = null;
            isProcessing = false;
            OnLoadingFinish?.Invoke();
        }

        private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
        {
            if (scene.path != scenePaths.Current)
                return;

            ComponentData[] componentsInScene = componentDatas
                .Where(component => component.scenePath == scene.path).ToArray();
            
            Array.ForEach(componentsInScene, LoadComponentValuesInEditMode);
            
            componentDatas = componentDatas.Except(componentsInScene).ToArray();
            
            EditorSceneManager.SaveScene(scene);
            
            EditorSceneManager.sceneOpened -= OnSceneOpened;
            
            LoadNextSceneComponentValues();
        }

        private static void LoadComponentValuesInEditMode(ComponentData componentData)
        {
            var component = ComponentDataUtility.FindComponent(componentData);

            if (component)
            {
                EditorJsonUtility.FromJsonOverwrite(componentData.componentDataJson, component);
                EditorUtility.SetDirty(component);
            }
        }
    }
}