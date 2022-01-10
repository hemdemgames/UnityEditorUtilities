using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.EditorTools;
using System;

namespace HemdemGames.EditorUtilities.Tools
{
    [EditorTool("Pivot Move", typeof(Transform))]
    internal class PivotMove : EditorTool
    {
        [SerializeField]
        Texture2D m_ToolIcon;

        private GUIContent m_IconContent;
        public override GUIContent toolbarIcon => m_IconContent;

        private Vector3 position;
        private Transform selection;
        private Quaternion rotation;

        private void OnEnable()
        {
            m_IconContent = new GUIContent("Pivot Move", m_ToolIcon, "Pivot Move Tool");
        }

        public override void OnActivated()
        {
            Selection.selectionChanged += OnSelectionChanged;
            Undo.undoRedoPerformed += OnSelectionChanged;
            OnSelectionChanged();
        }

        public override void OnWillBeDeactivated()
        {
            Selection.selectionChanged -= OnSelectionChanged;
            Undo.undoRedoPerformed -= OnSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            selection = Selection.activeTransform;
            position = selection ? selection.position : Vector3.zero;
        }

        public override void OnToolGUI(EditorWindow window)
        {
            EditorGUI.BeginChangeCheck();

            bool isGlobalPivotSelected = UnityEditor.Tools.pivotRotation == PivotRotation.Global;
            rotation = isGlobalPivotSelected ? Quaternion.identity : selection.rotation;
            position = Handles.PositionHandle(position, rotation);

            if (EditorGUI.EndChangeCheck())
            {
                Vector3 delta = position - selection.position;
                Undo.RecordObject(selection, "Pivot Move Tool");
                Undo.RecordObjects(selection.GetChildren(), "Pivot Move Tool");
                selection.Move(delta, false);
            }
        }
    }
}