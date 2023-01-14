using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Manager.General
{
    public class ManagerContainer : MonoBehaviour
    {
        
        #region Variables

        private static ManagerContainer instance;
        public static ManagerContainer Instance=>instance;


        [SerializeField] private GameManager gameManager;
        public GameManager GameManager => gameManager;

        [SerializeField] private UIManager uiManager;
        public UIManager UIManager => uiManager;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            StartManagers();
        }
        
        private void OnDestroy()
        {
            DestroyManagers();
        }

        #endregion
        
        #region ManagerProcess

        private void StartManagers()
        {
            gameManager.OnStarted();
            uiManager.OnStarted();
        }

        private void DestroyManagers()
        {
            gameManager.OnDestroyed();
            uiManager.OnDestroyed();
        }

        #endregion
    }
}