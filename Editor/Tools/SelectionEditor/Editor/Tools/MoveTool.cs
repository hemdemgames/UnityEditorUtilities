using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

namespace HemdemGames.EditorUtilities.Tools.SelectionEditor
{
    internal class MoveTool : ICustomTool
    {
        private Transform newParent;

        public bool GUIButton()
        {
            return GUILayout.Button("Move");
        }

        public void DrawGUI()
        {
            DrawTitle();
            DrawNewParentField();
            DrawApplyButton();
        }

        private void DrawNewParentField()
        {
            newParent = (Transform)EditorGUILayout.ObjectField("Move To", newParent, typeof(Transform), true);
        }

        private static void DrawTitle()
        {
            GUILayout.Label("MOVE SELECTEDS", EditorStyles.boldLabel);
        }

        private void DrawApplyButton()
        {
            GUI.enabled = newParent != null;
            if (GUILayout.Button("MOVE"))
            {
                ApplyOnClick();
            }
            GUI.enabled = true;
        }

        private void ApplyOnClick()
        {
            var selections = OrderedSelection.GetSelections();

            foreach (var selection in selections)
            {
                Undo.SetTransformParent(selection.transform, newParent, "Move");
            }
        }

        public void OnActivated()
        {

        }

        public void OnDeactivated()
        {

        }
    }
}