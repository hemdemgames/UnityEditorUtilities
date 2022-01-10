using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using JsonUtility = HemdemGames.Utilities.JsonUtility;
using Object = UnityEngine.Object;

namespace HemdemGames.EditorUtilities.Tools.SelectionEditor
{
    internal class SelectionSaveTool : ICustomTool
    {
        private List<SelectionGroup> selectionGroups = new List<SelectionGroup>();
        private string selectionName;
        private Vector2 scroll;

        private void SaveCollection()
        {
            string json = JsonUtility.ToJson(selectionGroups);
            EditorPrefs.SetString("custom_selections", json);
        }

        private void LoadCollection()
        {
            string defaultJson = JsonUtility.ToJson(new List<SelectionGroup>());
            string json = EditorPrefs.GetString("custom_selections", defaultJson);
            JsonUtility.FromJson<List<SelectionGroup>>(json);
        }
        
        public void DrawGUI()
        {
            GUILayout.Space(5);
            EditorGUILayout.LabelField("Selection Store Tool", EditorStyles.boldLabel);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            using (new EditorGUILayout.HorizontalScope())
            {
                selectionName = EditorGUILayout.TextField("Selection Name", selectionName);
            
                if (GUILayout.Button("Save Selection", GUILayout.ExpandWidth(false)))
                {
                    SaveSelection(selectionName, OrderedSelection.GetSelections());
                    selectionName = string.Empty;
                    GUI.FocusControl(null);
                } 
            }
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField("Saved Selections", EditorStyles.boldLabel);
            
            using (var scope = new EditorGUILayout.ScrollViewScope(scroll, GUILayout.MaxHeight(100)))
            {
                scroll = scope.scrollPosition;
                
                for (int i = 0; i < selectionGroups.Count; i++)
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(selectionGroups[i].name ?? "Saved Selection " + i);

                        if (GUILayout.Button("Load"))
                        {
                            LoadSelection(selectionGroups[i]);
                        }

                        if (GUILayout.Button("Delete"))
                        {
                            DeleteSelection(selectionGroups[i]);
                            return;
                        }
                    }
                }
            }
        }

        private void SaveSelection(string name, GameObject[] gameObjects)
        {
            SelectionGroup selectionGroup = new SelectionGroup();
            selectionGroup.name = name;
            selectionGroup.gameObjectInstanceIDs = gameObjects.Select(gameObject => gameObject.GetInstanceID()).ToArray();
            selectionGroups.Add(selectionGroup);
            SaveCollection();
        }

        private void DeleteSelection(SelectionGroup selectionGroup)
        {
            selectionGroups.Remove(selectionGroup);
            SaveCollection();
        }
        
        private GameObject[] GetSelectedGameObjects(SelectionGroup selectionGroup)
        {
            return GetGameObjectsFromInstanceIDs(selectionGroup.gameObjectInstanceIDs);
        }

        private void LoadSelection(SelectionGroup selectionGroup)
        {
            OrderedSelection.RemoveAll();
            OrderedSelection.AddRange(GetSelectedGameObjects(selectionGroup));
        }

        private GameObject[] GetGameObjectsFromInstanceIDs(int[] instanceIDs)
        {
            return instanceIDs.Select(InstanceIDToGameObject).Where(IsNotNull).ToArray();
        }

        private GameObject InstanceIDToGameObject(int instanceID)
        {
            return UnityEditor.EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        }

        private bool IsNotNull(object item)
        {
            return item != null;
        }

        public void OnActivated()
        {
            LoadCollection();
        }

        public void OnDeactivated()
        {
            
        }

        public bool GUIButton()
        {
            return GUILayout.Button("Selection Store");
        }
    }

    [Serializable]
    public class SelectionGroup
    {
        public string name;
        public int[] gameObjectInstanceIDs = new int[0];
    }
}