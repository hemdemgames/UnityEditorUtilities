using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HemdemGames.EditorUtilities.Tools
{
    internal static class ObjectUtility
    {
        public static int GetLocalIdentfierId(this Object obj)
        {
            PropertyInfo inspectorModeInfo =
                typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
 
            SerializedObject serializedObject = new SerializedObject(obj);
            inspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);
 
            SerializedProperty localIdProp =
                serializedObject.FindProperty("m_LocalIdentfierInFile");   //note the misspelling!
 
            return localIdProp.intValue;
        }
    }
}