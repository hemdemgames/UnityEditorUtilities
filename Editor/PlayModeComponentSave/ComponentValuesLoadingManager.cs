using HemdemGames.EditorUtilities.EditorSceneUtils;
using UnityEditor;
using UnityEngine;

namespace HemdemGames.EditorUtilities.PlayModeComponentSave
{
    internal static class ComponentValuesLoadingManager
    {
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplicationEvents.OnEnteredEditMode -= LoadAllComponentValuesInEditMode;
            EditorApplicationEvents.OnEnteredEditMode += LoadAllComponentValuesInEditMode;
        }

        private static void LoadAllComponentValuesInEditMode()
        {
            if (Application.isPlaying)
            {
                Debug.LogError("run only edit mode");
                return;
            }

            if (!ComponentSaveManager.HasAnyComponentSaved())
                return;

            EditorSceneManagerUtility.SaveLoadedScenesIfUserWantsTo();
            EditorSceneSnapshotManager.CaptureSnapshot();
            
            ComponentValuesLoader.OnLoadingFinish -= OnComponentValuesLoadingFinished;
            ComponentValuesLoader.OnLoadingFinish += OnComponentValuesLoadingFinished;
            ComponentValuesLoader.LoadComponentValues(ComponentSaveManager.GetAllComponentDatas());
        }

        private static void OnComponentValuesLoadingFinished()
        {
            ComponentValuesLoader.OnLoadingFinish -= OnComponentValuesLoadingFinished;
            ComponentSaveManager.Clear();
            EditorSceneSnapshotManager.LoadLastSnapshot();
        }
    }
}