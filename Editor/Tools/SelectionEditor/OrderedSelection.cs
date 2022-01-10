using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using PlasticGui.Help.Conditions;
using UnityEngine.PlayerLoop;
using Object = UnityEngine.Object;

namespace HemdemGames.EditorUtilities.Tools.SelectionEditor
{
    public static class OrderedSelection
    {
        private static List<GameObject> selections = new List<GameObject>();
        
        public static event Action OnSelectionChanged;

        [InitializeOnLoadMethod]
        static void Initialize()
        {
            Selection.selectionChanged -= SyncSelections;
            Selection.selectionChanged += SyncSelections;
            SyncSelections();
        }

        private static void SyncSelections()
        {
            selections = selections.Except(GetOldSelections()).ToList();
            selections.AddRange(GetNewSelections());
            OnSelectionChanged?.Invoke();
        }

        public static void Add(GameObject gameObject)
        {
            selections.Add(gameObject);

            UpdateEditorSelections();
        }

        public static void AddRange(IEnumerable<GameObject> gameObjects)
        {
            IEnumerator enumerator = gameObjects.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                selections.Add(enumerator.Current as GameObject);
            }

            UpdateEditorSelections();
        }

        public static void MoveUp(GameObject gameObject)
        {
            Move(gameObject, -1);
        }

        public static void MoveDown(GameObject gameObject)
        {
            Move(gameObject, 1);
        }

        public static void Move(GameObject gameObject, int amount)
        {
            int position = selections.IndexOf(gameObject);
            bool contains = (position >= 0);
            
            if (contains)
            {
                int newPosition = (position + amount) % selections.Count;

                if (newPosition < 0)
                {
                    newPosition += selections.Count;
                }

                selections.RemoveAt(position);
                selections.Insert(newPosition, gameObject);
            }
            
            UpdateEditorSelections();
        }

        public static void Remove(GameObject gameObject)
        {
            selections.Remove(gameObject);
            UpdateEditorSelections();
        }

        public static void RemoveAll()
        {
            selections.Clear();
            
            UpdateEditorSelections();
        }
        
        public static void RemoveRange(IEnumerable<GameObject> gameObjects)
        {
            IEnumerator enumerator = gameObjects.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                selections.Remove(enumerator.Current as GameObject);
            }

            UpdateEditorSelections();
        }
        
        private static GameObject[] GetOldSelections()
        {
            return selections.Except(Selection.gameObjects).ToArray();
        }

        private static GameObject[] GetNewSelections()
        {
            return Selection.gameObjects.Except(selections).ToArray();
        }

        public static GameObject[] GetSelections()
        {
            return selections.ToArray();
        }

        private static void UpdateEditorSelections()
        {
            Selection.objects = selections.ToArray();
        }
    }
}