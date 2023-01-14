using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Manager.General;
using Game.Scripts.Vehicle;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    #region Variables

    private PlayerController playerController;
    private PlayerVehicle playerVehicle;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerVehicle = GetComponent<PlayerVehicle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            playerController.wreckingBallController.SpinStart();
            other.GetComponent<PowerUpItem>().DestroyAction();
        }

        if (other.transform.CompareTag("Space"))
        {
            ManagerContainer.Instance.UIManager.OnLevelFailed();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            playerVehicle.isForwardMoving = true;
        }
    }

    #endregion
    
}
