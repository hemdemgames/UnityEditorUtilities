using UnityEngine;
using System.Collections;

namespace HemdemGames.EditorUtilities.Tools
{
    internal static class TransformUtility
    {
        public static void Move(this Transform transform, Vector3 moveDelta, bool moveChildren)
        {
            transform.position += moveDelta;

            if (!moveChildren)
            {
                foreach (Transform child in transform)
                {
                    child.position -= moveDelta;
                }
            }
        }

        public static Transform[] GetChildren(this Transform transform)
        {
            var children = new Transform[transform.childCount];

            for (int i = 0; i < children.Length; i++)
            {
                children[i] = transform.GetChild(i);
            }

            return children;
        }
    }
}