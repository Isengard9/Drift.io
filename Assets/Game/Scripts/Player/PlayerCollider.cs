using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            Debug.Log("player kutuya değdi");
            playerController.ballController.RotateTarget();
            other.GetComponent<PowerUpItem>().DestroyAction();
        }
    }
}
