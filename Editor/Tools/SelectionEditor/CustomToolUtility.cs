using UnityEngine;
using System.Collections;
using System;
using System.Linq;

namespace HemdemGames.EditorUtilities.Tools.SelectionEditor
{
    internal static class CustomToolUtility
    {
        public static ICustomTool[] FindAllTools()
        {
            var types = FindToolTypes();
            var tools = new ICustomTool[types.Length];

            for (int i = 0; i < types.Length; i++)
            {
                tools[i] = Activator.CreateInstance(types[i]) as ICustomTool;
            }

            return tools;
        }

        private static Type[] FindToolTypes()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(ICustomTool).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
                .ToArray();
        }
    }
}