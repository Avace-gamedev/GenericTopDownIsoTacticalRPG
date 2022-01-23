using System;
using UnityEngine;

namespace Scripts
{
    public class RootGameObject : MonoBehaviour, IRootGameObjectProvider
    {
        public static RootGameObject Instance { get; private set; }

        public GameObject Root => gameObject;

        public void Awake()
        {
            EnsureGameObjectIsEmpty();

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                throw new InvalidOperationException($"{nameof(RootGameObject)} has been awaken twice");
            }
        }

        private void EnsureGameObjectIsEmpty()
        {
            if (transform.childCount > 0)
            {
                throw new InvalidOperationException("The Root game object MUST have no children");
            }
        }
    }
}