using System.Linq;
using HemdemGames.EditorUtilities.Tools;
using UnityEditor;
using UnityEngine;

namespace HemdemGames.EditorUtilities.PlayModeComponentSave
{
    internal static class ComponentDataUtility
    {
        internal static Component FindComponent(ComponentData componentData)
        {
            var objects = (Component[]) GameObject.FindObjectsOfType(componentData.componentType, true);
            return objects
                .Where(obj => {
                    return (obj.gameObject.name == componentData.gameObjectName)
                           && obj.gameObject.CompareTag(componentData.gameObjectTag)
                           && ObjectUtility.GetLocalIdentfierId(obj) == componentData.localIdentfierId;
                }).First();
        }

        internal static ComponentData GetComponentData(Component component)
        {
            return new ComponentData()
            {
                scenePath = component.gameObject.scene.path,
                localIdentfierId = component.GetLocalIdentfierId(),
                gameObjectName = component.gameObject.name,
                componentType = component.GetType(),
                gameObjectTag = component.gameObject.tag,
                componentDataJson = EditorJsonUtility.ToJson(component)
            };
        }
    }
}