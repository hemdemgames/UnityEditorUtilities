using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HemdemGames.EditorUtilities.Tools.SelectionEditor
{
    internal class SelectionEditorWindow : EditorWindow
    {
        private Vector2 scrollPosition;
        private ICustomTool selectedTool;
        private ICustomTool[] tools = new ICustomTool[0];

        [MenuItem("Tools/Hemdem Games/Selection Editor", priority = 2)]
        static void OpenWindow()
        {
            GetWindow<SelectionEditorWindow>("Selection Editor");
        }

        private void OnEnable()
        {
            tools = CustomToolUtility.FindAllTools();
            OrderedSelection.OnSelectionChanged += Repaint;
        }

        private void OnGUI()
        {
            DrawToolbar();
            DrawSelectedTool();
            DrawSelections();
        }

        private void DrawSelections()
        {
            using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPosition, EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Selections", EditorStyles.boldLabel);
                
                var selections = OrderedSelection.GetSelections();
                foreach (var selection in selections)
                {
                    using (new EditorGUILayout.VerticalScope())
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Label(selection.name, GUILayout.Height(20));
                            if (GUILayout.Button("↑", GUILayout.ExpandWidth(false)))
                            {
                                OrderedSelection.MoveUp(selection);
                            }

                            if (GUILayout.Button("↓", GUILayout.ExpandWidth(false)))
                            {
                                OrderedSelection.MoveDown(selection);

                            }
                            
                            
                            if (GUILayout.Button("focus", GUILayout.Width(60), GUILayout.Height(20)))
                            {
                                EditorUtility.FocusSceneObject(selection);
                            }

                            if (GUILayout.Button("deselect", GUILayout.Width(75), GUILayout.Height(20)))
                            {
                                OrderedSelection.Remove(selection);
                                return;
                            }
                        }
                    }
                    
                    GUILayout.Space(-5);
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    GUILayout.Space(-5);
                }
            }
        }


        private void DrawSelectedTool()
        {
            if (selectedTool != null)
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    selectedTool.DrawGUI();
                    if (GUILayout.Button("CLOSE TOOL"))
                    {
                        SelectTool(null);
                    }
                }
            }
        }

        private void DrawToolbar()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                foreach (var tool in tools)
                {
                    if (tool.GUIButton())
                    {
                        SelectTool(tool);
                    }
                }
            }
        }

        private void SelectTool(ICustomTool tool)
        {
            if (selectedTool != null)
            {
                selectedTool.OnDeactivated();
            }

            selectedTool = tool;

            if (selectedTool != null)
            {
                selectedTool.OnActivated();
            }
        }

        private void OnDisable()
        {
            OrderedSelection.OnSelectionChanged -= Repaint;
        }
    }
}