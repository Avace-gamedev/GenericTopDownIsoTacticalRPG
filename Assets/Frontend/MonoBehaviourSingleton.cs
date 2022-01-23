using System;
using UnityEngine;

namespace Frontend
{
    public class MonoBehaviourSingleton: MonoBehaviour, IMonoBehaviourProvider
    {
        public static MonoBehaviourSingleton Instance { get; private set; }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                throw new InvalidOperationException($"{nameof(MonoBehaviourSingleton)} has been awaken twice");
            }
        }

        MonoBehaviour IMonoBehaviourProvider.GetMonoBehaviour()
        {
            return this;
        }
    }
}