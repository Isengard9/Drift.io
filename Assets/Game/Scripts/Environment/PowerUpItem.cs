using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Interface;
using UnityEngine;

public class PowerUpItem : MonoBehaviour, IDestroyable
{

    #region Variables

    [SerializeField] private GameObject parachute;
    
    private Rigidbody rb;

    #endregion

    #region MonoBehaviour

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            //Debug.Log("kutu yere degdi");
            rb.isKinematic = true;
            parachute.SetActive(false);
        }
    }

    #endregion


    #region DestroyAction

    public void DestroyAction()
    {
        
    }

    #endregion
}
