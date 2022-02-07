using System.Collections.Generic;
using System.Linq;
using HemdemGames.EditorUtilities.Tools;
using UnityEngine;

namespace HemdemGames.EditorUtilities.PlayModeComponentSave
{
    internal static class ComponentSaveManager
    {
        private static List<ComponentData> componentDatas = new List<ComponentData>();
        
        internal static void SaveComponent(Component component)
        {
            componentDatas.Add(ComponentDataUtility.GetComponentData(component));
        }

        internal static void Clear() => componentDatas.Clear();
        internal static ComponentData[] GetAllComponentDatas() => componentDatas.ToArray();
        internal static bool HasAnyComponentSaved() => componentDatas.Count > 0;

        internal static bool IsComponentSaved(Component component)
        {
            return GetComponentData(component) != null;
        }

        internal static ComponentData GetComponentData(Component component)
        {
            return GetComponentData(component.GetLocalIdentfierId());
        }

        internal static ComponentData GetComponentData(int localIdentfierId)
        {
            return componentDatas
                .FirstOrDefault(componentData => componentData.localIdentfierId == localIdentfierId);
        }
        
        internal static void DeleteComponent(Component component)
        {
            int componentLocalIdentfierId = component.GetLocalIdentfierId();
            componentDatas.RemoveAll(componentData => componentData.localIdentfierId == componentLocalIdentfierId);
        }
    }
}