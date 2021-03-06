using System;
using Backend.Kernel.DependencyInjection;
using UnityEngine;

namespace Scripts.Utils
{
    public static class GameObjectExtensions
    {
        public static readonly IRootGameObjectProvider RootProvider = Injector.Get<IRootGameObjectProvider>();

        public static GameObject CreateChildWithComponents(this GameObject parent, string name, params Type[] components)
        {
            GameObject result = new GameObject(name, components);
            result.transform.parent = parent.transform;

            return result;
        }

        public static GameObject CreateChildWithComponents(this GameObject parent, params Type[] components) =>
            parent.CreateChildWithComponents(null, components);

        public static GameObject CreateChild(this GameObject parent, string name = null) =>
            parent.CreateChildWithComponents(name);
    }
}