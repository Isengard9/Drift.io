using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Manager;
using UnityEngine;

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
        //TODO: Restart game functions
    }

    public void NextLevel()
    {
        //TODO: Next level game function
    }
}
