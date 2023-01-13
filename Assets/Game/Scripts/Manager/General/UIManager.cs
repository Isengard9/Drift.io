using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Manager.General
{
    public class UIManager : Manager
    {
        #region Variables

        [Header("Panels")] [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject lostPanel;

        [Header("Buttons")] 
        
        [SerializeField] private Button StartButton;
        [SerializeField] private Button RestartButton;
        [SerializeField] private Button NextButton;

        #endregion

        #region Button Function

        private void StartButtonClicked()
        {
            startPanel.SetActive(false);
            gamePanel.SetActive(true);

            ManagerContainer.Instance.GameManager.StartGame();
        }

        private void RestartButtonClicked()
        {
            ManagerContainer.Instance.GameManager.RestartGame();
        }

        private void NextLevelButtonClicked()
        {
            ManagerContainer.Instance.GameManager.NextLevel();
        }

        #endregion


        #region On Started | Destroyed

        public override void OnStarted()
        {
            AddListener();
        }

        public override void OnDestroyed()
        {
            RemoveListener();
        }

        #endregion


        #region Add | Remove Listeners

        protected override void AddListener()
        {
            base.AddListener();

            StartButton.onClick.AddListener(StartButtonClicked);
            // NextButton.onClick.AddListener(NextLevelButtonClicked);
            // RestartButton.onClick.AddListener(RestartButtonClicked);
        }

        protected override void RemoveListener()
        {
            base.RemoveListener();
            StartButton.onClick.RemoveListener(StartButtonClicked);
            // NextButton.onClick.RemoveListener(NextLevelButtonClicked);
            // RestartButton.onClick.RemoveListener(RestartButtonClicked);
        }

        #endregion
    }
}