using UnityEngine;
using System.Collections;

namespace HemdemGames.EditorUtilities.Tools
{
    internal static class BoundsUtility
    {
        public static Bounds CalculateBounds(GameObject gameObject)
        {
            var bounds = new Bounds(gameObject.transform.position, Vector3.zero);
            var renderers = gameObject.GetComponentsInChildren<Renderer>();

            foreach (var renderer in renderers)
            {
                bounds.Encapsulate(renderer.bounds);
            }

            return bounds;
        }
    }
}