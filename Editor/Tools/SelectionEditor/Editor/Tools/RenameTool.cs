using UnityEditor;
using UnityEngine;

namespace HemdemGames.EditorUtilities.Tools.SelectionEditor
{
    internal class RenameTool : ICustomTool
    {
        private string nameFormat = "GameObject {NUMBER}";
        private int startNumber = 0;
        private string numberFormat = "D2";
        
        public void DrawGUI()
        {
            DrawTitle();
            DrawContents();
        }

        private void DrawContents()
        {
            nameFormat = EditorGUILayout.TextField("Name Format", nameFormat);
            startNumber = EditorGUILayout.IntField("Start Number", startNumber);
            numberFormat = EditorGUILayout.TextField("Number Format", numberFormat);    
            
            if (GUILayout.Button("Rename All"))
            {
                RenameAll();
            }
        }

        private static void DrawTitle()
        {
            GUILayout.Label("Rename Tool", EditorStyles.boldLabel);
        }

        private void RenameAll()
        {
            Undo.RecordObjects(UnityEditor.Selection.gameObjects, "Rename");

            GameObject[] selections = OrderedSelection.GetSelections();
            
            for (int i = 0; i < selections.Length; i++)
            {
                int number = startNumber + i;
                selections[i].name = nameFormat.Replace("{NUMBER}", number.ToString(numberFormat));
            }
        }

        public void OnActivated()
        {
            
        }

        public void OnDeactivated()
        {
            
        }

        public bool GUIButton()
        {
            return GUILayout.Button("Renamer");
        }
    }
}