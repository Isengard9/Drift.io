using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Instance

    public static GameManager instance;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    #region Booleans
    
    public static bool isGameStarted = false;
    public static bool isGameEnded = false;

    #endregion

    #region Canvas

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject lostPanel;

    #endregion


    #region Buttons

    public void StartButton()
    {
        isGameStarted = true;
        isGameEnded = false;
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    #endregion
}
