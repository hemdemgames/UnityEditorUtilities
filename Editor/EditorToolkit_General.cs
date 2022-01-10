using UnityEngine;
using System.Collections;
using UnityEditor;

namespace HemdemGames.EditorUtilities.Tools
{
    internal class EditorToolkit_General : Editor
    {
        [MenuItem("GameObject/Editor Toolkit/", priority = 10)]
        static void GameObjectToolkit() { }

        [MenuItem("Viwodio/Editor Toolkit/", priority = 1)]
        static void MenuToolkit() { }
    }
}