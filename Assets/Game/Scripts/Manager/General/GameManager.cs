using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager
{

    #region Booleans
    
    public static bool IsGameStarted = false;
    public static bool IsGameEnded = false;

    #endregion
    
    #region On Started | Destroyed

    public override void OnStarted()
    {
        ResetGame();
    }

    public override void OnDestroyed()
    {
        ResetGame();
    }

    #endregion

    #region Game

    public void StartGame()
    {
        IsGameStarted = true;
        IsGameEnded = false;
    }

    public void EndGame()
    {
        IsGameEnded = true;
    }

    public void ResetGame()
    {
        IsGameStarted = false;
        IsGameEnded = false;
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
}
