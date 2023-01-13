using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpController : MonoBehaviour
{

    #region Variables

    [SerializeField] private GameObject item;

    [SerializeField] private float clampX, clampY, clampZ;

    private float _timer = 10;
    
    #endregion

    #region Monobehaviour

    private void Update()
    {
        if (!GameManager.IsGameStarted || GameManager.IsGameEnded) return;
        
        CreationProcess();
    }

    #endregion

    #region CreateItem

    private void CreateItem()
    {
        var box = Instantiate(item, SetRandomPosition(), Quaternion.identity, transform);
    }

    private void CreationProcess()
    {
        if (_timer <= 0)
        {
            CreateItem();
            SetRandomTime();
        }

        else
        {
            _timer -= Time.deltaTime;
        }
    }

    #endregion

    #region SetRandom

    private Vector3 SetRandomPosition()
    {
        var posX = Random.Range(-clampX, clampX);
        var posZ = Random.Range(-clampZ, clampZ);

        var pos = new Vector3(posX, clampY, posZ);
        return pos;
    }

    private void SetRandomTime()
    {
        _timer = Random.Range(5, 25);
    }

    #endregion
}
