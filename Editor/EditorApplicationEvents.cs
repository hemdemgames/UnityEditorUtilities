using System;
using UnityEditor;

namespace HemdemGames.EditorUtilities
{
    internal static class EditorApplicationEvents
    {
        internal static event Action OnEnteredEditMode;

        [InitializeOnLoadMethod]
        static void Init()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            switch (obj)
            {
                case PlayModeStateChange.EnteredEditMode:
                    OnEnteredEditMode?.Invoke();
                    break;
            }
        }
    }
}