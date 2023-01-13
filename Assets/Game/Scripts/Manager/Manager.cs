using System;
using UnityEngine;

namespace Game.Scripts.Manager
{
    public abstract class Manager : MonoBehaviour, IManager
    {
        public abstract void OnStarted();
        public abstract void OnDestroyed();

        protected virtual void AddListener()
        {
            
        }

        protected virtual void RemoveListener()
        {
            
        }
    }
}